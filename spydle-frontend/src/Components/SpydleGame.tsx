import * as Phaser from "phaser";
import { useEffect } from "react";
import PreloaderScene from "../Game/GameScenes/PreloaderScene";
import GameplayScene from "../Game/GameScenes/GameplayScene";

const config = {
    type: Phaser.AUTO,
    width: 889,
    height: 702,
    parent: "spydle-game",
    scene: [PreloaderScene, GameplayScene]
};

export default function SpydleGame(){
    useEffect(() => {
        const game = new Phaser.Game(config);
        return(() => game.destroy(true));
    });
    return(
        <div id="spydle-game">
        </div>
    )
}