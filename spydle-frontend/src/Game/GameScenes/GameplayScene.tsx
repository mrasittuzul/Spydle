import * as Phaser from "phaser";
import type { GameplayData } from "./PreloaderScene";
import { Character } from "../Classes/Character";
import { Post } from "../../Helpers/RequestHelper";

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

        this.submitButton = this.add.image(100, 613, "white").setDisplaySize(150, 64).setTint(this.submitButtonActiveColor).setOrigin(0, 0);
        this.submitButtonText = this.add.text(175, 613 + 64/2, "Submit", {
            fontSize: "18px",
            color: "#000000"
        })
        .setOrigin(0.5)
        .on("pointerdown", this.onSubmitButtonClicked);

        this.statusText = this.add.text(175, 560, "Status", {
            fontSize: "18px",
            color: "#ffffff"
        }).setOrigin(0.5).setVisible(false);

        this.spawnCharacters();
        this.spawnSuspectGroups();
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

        this.suspectGroups[suspectGroupCount - 1].setTint(new Phaser.Display.Color(163, 189, 196)); // Final guess suspect group has a different color.
    }

    async onSubmitButtonClicked() : Promise<void> {
        if(this.isMakingRequest){
            return;
        }

        var currentSuspectGroup = this.suspectGroups[this.interrogationCount];
        if(currentSuspectGroup.suspectCodes.length < currentSuspectGroup.capacity){
            return;
        }

        this.isMakingRequest = true;
        var result = await Post<{isMatchFound: boolean, interrogationNumber: number}>("Character/CheckSuspects", 
            { SuspectCodes: currentSuspectGroup.characters.map((s: Character) => s.code) })
        this.isMakingRequest = false;
        if(result.status != 200){
            this.statusText.setText("Try submitting again");
            return;
        }

        var interrogationResponse = result.data as {isMatchFound: boolean, interrogationNumber: number};
        currentSuspectGroup.setResult(interrogationResponse.isMatchFound);
        this.interrogationCount = interrogationResponse.interrogationNumber;
    }
}