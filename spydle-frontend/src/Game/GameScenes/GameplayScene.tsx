import * as Phaser from "phaser";
import type { GameplayData } from "./PreloaderScene";
import { Character } from "../Classes/Character";

import maleOldOverlay from '../../assets/GameAssets/MaleOldOverlay.png';
import femaleOldOverlay from '../../assets/GameAssets/FemaleOldOverlay.png';
import eyeFill from '../../assets/GameAssets/EyeFill.png';
import eyeSockets from '../../assets/GameAssets/EyeSockets.png';
import femaleBase from '../../assets/GameAssets/FemaleBase.png';
import femaleOutline from '../../assets/GameAssets/FemaleOutline.png';
import maleBase from '../../assets/GameAssets/MaleBase.png';
import maleOutline from '../../assets/GameAssets/MaleOutline.png';

export default class GameplayScene extends Phaser.Scene {
    caseData: {rngSeed: number, includedCharacters: number[]} = {rngSeed:0, includedCharacters:[]};
    eyeColors: {red: number, green: number, blue: number}[] = [];
    skinColors: {red: number, green: number, blue: number}[] = [];
    characters: Character[] = [];

    constructor() {
        super({ key: "GameplayScene" });
    }

    init(data: GameplayData)
    {
        this.caseData = data.caseData,
        this.eyeColors = data.eyeColors,
        this.skinColors = data.skinColors
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
    }

    create() {
        this.spawnCharacters();
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
            this.characters.push(new Character(this, this.caseData.includedCharacters[i], posX, posY));
        }
    }
}