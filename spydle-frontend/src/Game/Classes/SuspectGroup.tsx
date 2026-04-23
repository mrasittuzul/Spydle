import * as Phaser from "phaser"
import type GameplayScene from "../GameScenes/GameplayScene";
import { Character } from "./Character";

export class SuspectGroup extends Phaser.GameObjects.Container{
    readonly scene: GameplayScene
    readonly capacity: number;

    background!: Phaser.GameObjects.Image;
    resultPip!: Phaser.GameObjects.Image;
    characters: Character[] = [];

    constructor(scene: GameplayScene, capacity: number, backgroundColor: Phaser.Display.Color, x: number, y: number) {
        super(scene, x, y);
        this.scene = scene;
        this.capacity = capacity;

        const containerElements: Phaser.GameObjects.GameObject[] = [];
        this.background = this.scene.add.image(0, 0, "white").setDisplaySize(300, 64).setOrigin(0, 0).setTint(backgroundColor.color);
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
    }
}