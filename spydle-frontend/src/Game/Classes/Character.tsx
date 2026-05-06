import * as Phaser from "phaser"
import type GameplayScene from "../GameScenes/GameplayScene";

export class Character extends Phaser.GameObjects.Container {
    readonly scene: GameplayScene
    
    code: number;
    characterTraits: CharacterTraits;
    
    isInteractive: boolean = false;
    isOutlineVisible: boolean = false;
    outlineColor: Phaser.Display.Color = new Phaser.Display.Color(255, 255, 255);

    baseImage!: Phaser.GameObjects.Image;
    eyeSockets!: Phaser.GameObjects.Image;
    eyeFill!: Phaser.GameObjects.Image;
    oldOverlay!: Phaser.GameObjects.Image;
    outline!: Phaser.GameObjects.Image;
    container!: Phaser.GameObjects.Container;

    constructor(scene: GameplayScene, code: number, x: number, y: number, interactable: boolean) {
        super(scene, x, y);
        this.scene = scene;

        const baseTexture = this.scene.textures.get("maleBase").getSourceImage();
        this.setSize(baseTexture.width, baseTexture.height);
        if(interactable){
            this.setInteractive(
                new Phaser.Geom.Rectangle(baseTexture.width / 2, baseTexture.height / 2, baseTexture.width, baseTexture.height),
                Phaser.Geom.Rectangle.Contains
            );
            this.on("pointerdown", this.onClicked);
            this.scene.events.on("suspectGroupHoverOn", this.onSuspectGroupHoverOn, this);
            this.scene.events.on("suspectGroupHoverOut", this.onSuspectGroupHoverOut, this);
            this.isInteractive = true;
        }

        this.code = code;
        this.createContainer();
        this.characterTraits = this.decodeCharacterTraitsFromCode(code);
        this.setCharacterTraits(this.characterTraits);

        //this.scene.input.enableDebug(this, 0xffff00);
    }

    decodeCharacterTraitsFromCode(code: number) : CharacterTraits {
        return {
            Sex:            (code & TraitMasks.Sex)            >> 0,
            Age:            (code & TraitMasks.Age)            >> 1,
            SkinColorIndex: (code & TraitMasks.SkinColorIndex) >> 2,
            EyeColorIndex:  (code & TraitMasks.EyeColorIndex)  >> 4,
        };
    }

    createContainer() : void {
        this.baseImage = this.scene.add.image(0, 0, "").setOrigin(0, 0);
        this.eyeSockets = this.scene.add.image(0, 0, "eyeSockets").setOrigin(0, 0);
        this.eyeFill = this.scene.add.image(0, 0, "eyeFill").setOrigin(0, 0);
        this.oldOverlay = this.scene.add.image(0, 0, "oldOverlay").setOrigin(0, 0);
        this.outline = this.scene.add.image(0, 0, "").setOrigin(0, 0).setVisible(false);

        this.add([this.baseImage, this.eyeSockets, this.eyeFill, this.oldOverlay, this.outline]);
        this.scene.add.existing(this);
    }

    setCode(code: number){
        this.code = code;
        this.setCharacterTraits(this.decodeCharacterTraitsFromCode(code));
    }

    setCharacterTraits(characterTraits: CharacterTraits){
        const baseTexture = characterTraits.Sex === 0 ? "maleBase" : "femaleBase";
        const outlineTexture = characterTraits.Sex === 0 ? "maleOutline" : "femaleOutline";
        const skinColor = this.scene.skinColors[characterTraits.SkinColorIndex]; 
        const eyeColor = this.scene.eyeColors[characterTraits.EyeColorIndex];

        this.baseImage.setTexture(baseTexture);
        this.outline.setTexture(outlineTexture);
        this.baseImage.setTint(new Phaser.Display.Color(skinColor.red, skinColor.green, skinColor.blue).color);
        this.eyeFill.setTint(new Phaser.Display.Color(eyeColor.red, eyeColor.green, eyeColor.blue).color);
        this.oldOverlay.setVisible(this.characterTraits.Age === 1);
    }

    setOutline(isVisible: boolean, color: Phaser.Display.Color){
        this.isOutlineVisible = isVisible;
        this.outlineColor = color;
        this.outline.setVisible(isVisible);
        this.outline.setTint(color.color);
    }

    onClicked(pointer: Phaser.Input.Pointer){
        this.scene.events.emit("onCharacterClicked", this);
    }

    onSuspectGroupHoverOn(suspectCodes: number[]){
        if(suspectCodes.indexOf(this.code) === -1){
            return;
        }
        this.setOutline(true, this.outlineColor);
    }
    
    onSuspectGroupHoverOut(suspectCodes: number[]){
        if(suspectCodes.indexOf(this.code) === -1){
            return;
        }
        this.setOutline(false, this.outlineColor);
    }
}

const TraitMasks = {
    Sex:            0b0000_0000_0000_0001,
    Age:            0b0000_0000_0000_0010,
    SkinColorIndex: 0b0000_0000_0000_1100,
    EyeColorIndex:  0b0000_0000_0011_0000,
};

type CharacterTraits = {
    Sex: number;
    Age: number;
    EyeColorIndex: number;
    SkinColorIndex: number;
}