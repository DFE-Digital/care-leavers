
import {Page, Locator, expect} from '@playwright/test';
import {QuestionnaireRunBasePage} from "./QuestionnaireRunBasePage";

export class QuestionnaireStartPage extends QuestionnaireRunBasePage {
    // ===== Locators =====    
    readonly startButton: Locator;
    public readonly gdsContentFulContent: Locator;
    public readonly startButtonLink: Locator;

    // ===== Constructor =====
    constructor(page: Page) {
        super(page);
        this.gdsContentFulContent = page.locator('gds-contentful-content');
        this.startButton = page.locator('.govuk-button govuk-button--start');
        this.startButtonLink = page.locator('a[href*="/en/question-1"]');
    }

    // ===== Assertions =====
    async assertStructure(){
        await expect(this.gdsContentFulContent);
        await expect(this.startButton); 
        await expect(this.startButtonLink);       
    }   
}