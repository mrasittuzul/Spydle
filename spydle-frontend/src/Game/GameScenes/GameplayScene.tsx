import * as Phaser from "phaser";
import type { GameplayData } from "./PreloaderScene";
import { Character } from "../Classes/Character";
import { Get, Post } from "../../Helpers/RequestHelper";

import maleOldOverlay from '../../assets/GameAssets/MaleOldOverlay.png';
import femaleOldOverlay from '../../assets/GameAssets/FemaleOldOverlay.png';
import eyeFill from '../../assets/GameAssets/EyeFill.png';
import eyeSockets from '../../assets/GameAssets/EyeSockets.png';
import femaleBase from '../../assets/GameAssets/FemaleBase.png';
import femaleOutline from '../../assets/GameAssets/FemaleOutline.png';
import maleBase from '../../assets/GameAssets/MaleBase.png';
import maleOutline from '../../assets/GameAssets/MaleOutline.png';
import white from '../../assets/GameAssets/white.png';
import { SuspectGroup } from "../Classes/SuspectGroup";

export default class GameplayScene extends Phaser.Scene {
    caseData: {rngSeed: number, includedCharacters: number[]} = {rngSeed:0, includedCharacters:[]};
    eyeColors: {red: number, green: number, blue: number}[] = [];
    skinColors: {red: number, green: number, blue: number}[] = [];
    characters: Character[] = [];
    spyCharacter: Character = null!;
    suspectGroups: SuspectGroup[] = [];
    interrogationCount: number = 0;
    submitButton!: Phaser.GameObjects.Image;
    submitButtonText!: Phaser.GameObjects.Text;
    statusText!: Phaser.GameObjects.Text;

    submitButtonActiveColor: number = 0x1e93ee;
    submitButtonInactiveColor: number = 0x104065;
    isMakingRequest: boolean = false;

    constructor() {
        super({ key: "GameplayScene" });
    }

    init(data: GameplayData)
    {
        this.caseData = data.caseData;
        this.eyeColors = data.eyeColors;
        this.skinColors = data.skinColors;

        this.events.on("suspectToggled", this.onSuspectToggled, this);
    }

    preload() {
        this.load.image('femaleOldOverlay', femaleOldOverlay);
        this.load.image('maleOldOverlay', maleOldOverlay);
        this.load.image('eyeFill', eyeFill);
        this.load.image('eyeSockets', eyeSockets);
        this.load.image('femaleBase', femaleBase);
        this.load.image('femaleOutline', femaleOutline);
        this.load.image('maleBase', maleBase);
        this.load.image('maleOutline', maleOutline);
        this.load.image('white', white);
    }

    create() {
        // Character area separator
        const separator  = this.add.graphics();
        separator.fillStyle(0xffffff);
        separator.fillRect(350, 0, 5, this.game.config.height as number);

        this.spawnCharacters();
        this.spawnSuspectGroups();

        this.statusText = this.add.text(175, 560, "", {
            fontSize: "16px",
            color: "#ffffff"
        }).setOrigin(0.5);

        this.submitButton = this.add.image(100, 613, "white")
            .setDisplaySize(150, 64)
            .setOrigin(0, 0)
            .setTint(this.submitButtonActiveColor)
            .setAlpha(0.5)
            .setInteractive(new Phaser.Geom.Rectangle(0, 0, 1, 1),
                Phaser.Geom.Rectangle.Contains)
            .on("pointerdown", this.onSubmitButtonClicked, this);
        this.submitButtonText = this.add.text(175, 613 + 64/2, "0" + "/" + this.suspectGroups[0].capacity, {
            fontSize: "18px",
            color: "#000000"
        }).setOrigin(0.5);
    }

    spawnCharacters() : void {
        const baseTexture = this.textures.get("maleBase").getSourceImage();
        const textureSize = { width: baseTexture.width, height: baseTexture.height };
        const startingPosition = {x: 375, y: 25};
        const columnCount = 8;
        const columnGap = 15;
        const rowGap = 20;

        for(let i = 0; i < this.caseData.includedCharacters.length; i++){
            const posX = startingPosition.x + (i % columnCount) * (textureSize.width + columnGap);
            const posY = startingPosition.y + Math.floor(i / columnCount) * (textureSize.height + rowGap)
            this.characters.push(new Character(this, this.caseData.includedCharacters[i], posX, posY, true));
        }

        // Spawn another character to show who the spy is at the end of the game.
        this.spyCharacter = new Character(this, 0, 151, 529, false);
        this.spyCharacter.on("pointerover", this.onSpyCharacterHoverOn, this);
        this.spyCharacter.on("pointerout", this.onSpyCharacterHoverOut, this);
        this.spyCharacter.setVisible(false);
    }

    spawnSuspectGroups() : void {
        const suspectGroupGap = 84;
        const startingPos = { x: 25, y: 25 };
        const suspectGroupCount = 6;
        const suspectCountPerGroup = 5;
        const backgroundColor = new Phaser.Display.Color(44, 44, 44);

        for(let i = 0; i < suspectGroupCount; i++){
            const suspectGroup = new SuspectGroup(this, i, suspectCountPerGroup, backgroundColor, startingPos.x, startingPos.y + suspectGroupGap * i);
            this.suspectGroups.push(suspectGroup);
        }

        this.suspectGroups[suspectGroupCount - 1].setTint(new Phaser.Display.Color(66, 66, 66)); // Final guess suspect group has a different color.
    }

    async onSubmitButtonClicked(){
        if(this.isMakingRequest){
            return;
        }

        var currentSuspectGroup = this.suspectGroups[this.interrogationCount];
        if(currentSuspectGroup.suspectCodes.length < currentSuspectGroup.capacity){
            return;
        }

        // In case the automatic spy character query fails and the user is asked to try again.
        if(this.interrogationCount === 6){
            await this.revealSpyCharacter();
            return;
        }

        this.isMakingRequest = true;
        this.statusText.setText("Interrogating...");
        var result = await Post<{isMatchFound: boolean, interrogationNumber: number}>("Character/CheckSuspects", 
            { SuspectCodes: currentSuspectGroup.suspectCodes })
        this.isMakingRequest = false;
        this.statusText.setText("");
        if(result.status != 200){
            this.statusText.setText("Try submitting again");
            return;
        }

        var interrogationResponse = result.data as {isMatchFound: boolean, interrogationNumber: number};
        currentSuspectGroup.setResult(interrogationResponse.isMatchFound);
        this.interrogationCount = interrogationResponse.interrogationNumber;

        // Get the spy character after the last interrogation.
        if(this.interrogationCount === 6){
            await this.revealSpyCharacter();
        }
    }

    async revealSpyCharacter(){
        if(this.interrogationCount === 6){
            this.isMakingRequest = true;
            this.statusText.setText("Getting Result...");
            var spyResult = await Get<{ spyCharacterCode: number }>("Character/GetSpy");
            this.isMakingRequest = false;
            this.statusText.setText("");

            if(spyResult.status === 200){
                this.spyCharacter.setCode((spyResult.data as { spyCharacterCode: number }).spyCharacterCode)
                this.spyCharacter.setInteractive(new Phaser.Geom.Rectangle(48/2, 64/2, 48, 64),
                    Phaser.Geom.Rectangle.Contains);
                this.spyCharacter.isInteractive = true;
                this.spyCharacter.setVisible(true);
            }
            else{
                this.statusText.setText("Please submit again.");
            }
        }
    }

    onSuspectToggled(){
        var currentSuspectGroup = this.suspectGroups[this.interrogationCount];
        var isSuspectGroupAtCapacity = currentSuspectGroup.suspectCodes.length == currentSuspectGroup.capacity;
        this.submitButton.setAlpha(isSuspectGroupAtCapacity ? 1 : 0.5);
        this.submitButtonText.setText(isSuspectGroupAtCapacity ? "Submit" : (currentSuspectGroup.suspectCodes.length + "/" + currentSuspectGroup.capacity))
    }

    onSpyCharacterHoverOn(){
        if(!this.spyCharacter.isInteractive){
            return;
        }
        this.characters.find(c => c.code == this.spyCharacter.code)?.setOutline(true, this.spyCharacter.outlineColor);
    }

    onSpyCharacterHoverOut(){
        if(!this.spyCharacter.isInteractive){
            return;
        }
        this.characters.find(c => c.code == this.spyCharacter.code)?.setOutline(false, this.spyCharacter.outlineColor);
    }
}