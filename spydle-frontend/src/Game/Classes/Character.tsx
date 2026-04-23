import * as Phaser from "phaser"
import type GameplayScene from "../GameScenes/GameplayScene";

export class Character {
    readonly scene: GameplayScene
    
    code: number;
    characterTraits: CharacterTraits;

    baseImage!: Phaser.GameObjects.Image;
    eyeSockets!: Phaser.GameObjects.Image;
    eyeFill!: Phaser.GameObjects.Image;
    oldOverlay!: Phaser.GameObjects.Image;
    outline!: Phaser.GameObjects.Image;
    container!: Phaser.GameObjects.Container;

    posX: number;
    posY: number;
    constructor(scene: GameplayScene, code: number, posX: number, posY: number) {
        this.scene = scene;
        this.code = code;
        this.posX = posX;
        this.posY = posY;
        this.characterTraits = this.DecodeCharacterTraitsFromCode(code);
        this.CreateContainer();
        this.SetCharacterTraits(this.characterTraits);
    }

    DecodeCharacterTraitsFromCode(code: number) : CharacterTraits {
        return {
            Sex:            (code & TraitMasks.Sex)            >> 0,
            Age:            (code & TraitMasks.Age)            >> 1,
            SkinColorIndex: (code & TraitMasks.SkinColorIndex) >> 2,
            EyeColorIndex:  (code & TraitMasks.EyeColorIndex)  >> 4,
        };
    }

    CreateContainer() : void {
        this.baseImage = this.scene.add.image(0, 0, "").setOrigin(0, 0);
        this.eyeSockets = this.scene.add.image(0, 0, "eyeSockets").setOrigin(0, 0);
        this.eyeFill = this.scene.add.image(0, 0, "eyeFill").setOrigin(0, 0);
        this.oldOverlay = this.scene.add.image(0, 0, this.characterTraits.Sex === 0 ? "maleOldOverlay" : "femaleOldOverlay").setOrigin(0, 0).setVisible(this.characterTraits.Age === 1);
        this.outline = this.scene.add.image(0, 0, "").setOrigin(0, 0).setVisible(false);

        this.container = this.scene.add.container(this.posX, this.posY, [ this.baseImage, this.eyeSockets, this.eyeFill, this.oldOverlay, this.outline ]);
    }

    SetCharacterTraits(characterTraits: CharacterTraits){
        const baseTexture = characterTraits.Sex === 0 ? "maleBase" : "femaleBase";
        const outlineTexture = characterTraits.Sex === 0 ? "maleOutline" : "femaleOutline";
        const skinColor = this.scene.skinColors[characterTraits.EyeColorIndex]; 
        const eyeColor = this.scene.eyeColors[characterTraits.EyeColorIndex];
        const isOld = characterTraits.Age === 1;
        const oldOverlayTexture = characterTraits.Age === 0 ? "maleOldOverlay" : "femaleOldOverlay";

        this.baseImage.setTexture(baseTexture);
        this.outline.setTexture(outlineTexture);
        this.baseImage.setTint(new Phaser.Display.Color(skinColor.red, skinColor.green, skinColor.blue).color);
        this.eyeFill.setTint(new Phaser.Display.Color(eyeColor.red, eyeColor.green, eyeColor.blue).color);
        this.oldOverlay.setVisible(isOld);
        this.oldOverlay.setTexture(oldOverlayTexture);
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