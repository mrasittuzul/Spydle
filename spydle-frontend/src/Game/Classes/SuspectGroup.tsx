import * as Phaser from "phaser"
import type GameplayScene from "../GameScenes/GameplayScene";
import { Character } from "./Character";

export class SuspectGroup extends Phaser.GameObjects.Container{
    readonly scene: GameplayScene
    readonly capacity: number;
    readonly groupIndex: number;

    background!: Phaser.GameObjects.Image;
    resultPip!: Phaser.GameObjects.Image;
    characters: Character[] = [];
    suspectCodes: number[] = [];

    constructor(scene: GameplayScene, groupIndex: number, capacity: number, backgroundColor: Phaser.Display.Color, x: number, y: number) {
        super(scene, x, y);
        this.scene = scene;
        this.groupIndex = groupIndex;
        this.capacity = capacity;

        const containerElements: Phaser.GameObjects.GameObject[] = [];
        const width = 300;
        const height = 64;
        this.background = this.scene.add.image(0, 0, "white").setDisplaySize(width, height).setOrigin(0, 0).setTint(backgroundColor.color);
        this.resultPip = this.scene.add.image(305, 25, "white").setDisplaySize(15, 15).setOrigin(0, 0);
        containerElements.push(this.background);

        const columnGap = 15;
        const baseCharacterTextureWidth = scene.textures.get("maleBase").getSourceImage().width;
        for(let i = 0; i < capacity; i++){
            const character = new Character(scene, 0, (baseCharacterTextureWidth + columnGap) * i, 0, false);
            character.setVisible(false);
            this.characters.push(character);
            containerElements.push(character);
        }

        this.add([ this.background, this.resultPip, ...this.characters]);
        this.scene.add.existing(this);

        this.setInteractive(
            new Phaser.Geom.Rectangle(0, 0, width, height),
            Phaser.Geom.Rectangle.Contains
        );
        this.on("pointerover", this.onPointerOver, this);
        this.on("pointerout", this.onPointerOut, this);
        this.scene.events.addListener("onCharacterClicked", this.onCharacterClicked, this);

        //this.scene.input.enableDebug(this, 0xffff00);
    }

    setTint(color: Phaser.Display.Color){
        this.background.setTint(color.color);
    }

    toggleSuspect(suspect: Character) : void{
        var isSuspectInGroup: boolean = this.suspectCodes.find((s: number) => s === suspect.code) !== undefined;
        if(this.suspectCodes.length === this.capacity && !isSuspectInGroup){
            return;
        }

        if(isSuspectInGroup){
            this.suspectCodes.forEach( (item, index) => {
                if(item === suspect.code) this.suspectCodes.splice(index,1);
            });
        }
        else{
            this.suspectCodes.push(suspect.code);
        }

        this.characters.forEach((element, index) => {
            if(index < this.suspectCodes.length){
                element.setCode(this.suspectCodes[index]);
                element.setVisible(true);
            }
            else{
                element.setVisible(false);
            }
        });

        this.scene.events.emit("suspectToggled");
    }

    setResult(isMatchFound: boolean){
        this.resultPip.setTint(new Phaser.Display.Color(isMatchFound ? 0 : 255, isMatchFound ? 255 : 0, 0).color);
    }

    onPointerOver(pointer: Phaser.Input.Pointer){
        this.scene.events.emit("suspectGroupHoverOn", this.suspectCodes);
    }

    onPointerOut(pointer: Phaser.Input.Pointer, hoveredObjects: Phaser.GameObjects.GameObject[]){
        this.scene.events.emit("suspectGroupHoverOut", this.suspectCodes);
    }

    onCharacterClicked(clickedCharacter: Character){
        if(this.scene.interrogationCount !== this.groupIndex){
            return;
        }
        this.toggleSuspect(clickedCharacter);
    }
}