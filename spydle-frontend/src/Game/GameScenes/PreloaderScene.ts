import * as Phaser from "phaser";
import { Get } from "../../Helpers/RequestHelper";

export type CaseData = {
    interrogationCount: number, 
    regularInterrogationSuspectCount: number,
    finalInterrogationSuspectCount: number,
    rngSeed: number, 
    includedCharacters: number[],
}

export type InterrogationsForTodaysCase = {
    interrogations: 
    { 
        dateTimeInMiliseconds: number, 
        suspects: number[], 
        foundMatchingTrait: boolean,
    }[]
}

export type RGBColor = {
    red: number,
    green: number,
    blue: number,
}

export type GameplayData = {
    caseData: CaseData,
    interrogationsForTodaysCase: InterrogationsForTodaysCase,
    eyeColors: RGBColor[],
    skinColors: RGBColor[],
}

export default class PreloaderScene extends Phaser.Scene {
    private progressBar!: Phaser.GameObjects.Graphics;
    private progressBox!: Phaser.GameObjects.Graphics;
    private statusText!: Phaser.GameObjects.Text;
    private retryText!: Phaser.GameObjects.Text;

    constructor() {
        super({ key: "PreloaderScene" });
    }

    create() {
        const centerX = this.cameras.main.width / 2;
        const centerY = this.cameras.main.height / 2;

        // Progress bar background
        this.progressBox = this.add.graphics();
        this.progressBox.fillStyle(0x222222);
        this.progressBox.fillRect(centerX - 160, centerY - 15, 320, 30);

        // Progress bar fill
        this.progressBar = this.add.graphics();

        // Status text
        this.statusText = this.add.text(centerX, centerY - 40, "Loading...", {
            fontSize: "18px",
            color: "#ffffff"
        }).setOrigin(0.5);

        // Retry text (hidden initially)
        this.retryText = this.add.text(centerX, centerY + 40, "Failed to load. Click to try again.", {
            fontSize: "16px",
            color: "#ff4444"
        }).setOrigin(0.5).setVisible(false).setInteractive();

        this.retryText.on("pointerdown", () => {
            this.retryText.setVisible(false);
            this.loadData();
        });

        this.time.delayedCall(0, this.loadData, [], this);
    }

    private updateProgress(progress: number, status: string) {
        this.progressBar.clear();
        this.progressBar.fillStyle(0x00ff00);
        this.progressBar.fillRect(
            this.cameras.main.width / 2 - 158,
            this.cameras.main.height / 2 - 13,
            304 * progress,
            26
        );
        this.statusText.setText(status);
    }

    private async loadData() {
        try {
            this.updateProgress(0, "Fetching case...");
            const caseResponse = await Get("Case/Today");
            if (caseResponse.status !== 200) throw new Error("Failed to fetch case");

            this.updateProgress(0.5, "Fetching case...");
            const interrogationsResponse = await Get("Interrogation/GetInterrogationsForTodaysCase");
            if (interrogationsResponse.status !== 200) throw new Error("Failed to fetch interrogations");

            this.updateProgress(0.6, "Fetching eye colors...");
            const eyeColorsResponse = await Get("Character/EyeColors");
            if (eyeColorsResponse.status !== 200) throw new Error("Failed to fetch eye colors");

            this.updateProgress(0.7, "Fetching skin colors...");
            const skinColorsResponse = await Get("Character/SkinColors");
            if (skinColorsResponse.status !== 200) throw new Error("Failed to fetch skin colors");

            this.updateProgress(1, "Done!");

            // Small delay so the player sees 100% before transitioning
            //await new Promise(resolve => setTimeout(resolve, 500));
            
            this.scene.start("GameplayScene", {
                caseData: caseResponse.data,
                interrogationsForTodaysCase: interrogationsResponse.data,
                eyeColors: eyeColorsResponse.data,
                skinColors: skinColorsResponse.data
            } as GameplayData);

        } catch (error) {
            this.statusText.setText("");
            this.retryText.setVisible(true);
        }
    }
}