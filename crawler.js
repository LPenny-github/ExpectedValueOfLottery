const retryCount = 3;
const delay = 1000;
let currentRetry = 0;

const url = "https://powerball.com";

var cheerio = require("cheerio");
const puppeteer = require("puppeteer");
const fs = require("fs");

Retry();

async function Retry(){

        try {
            getData();
                        
        } catch (error) {
            
            ++currentRetry;
            console.log("retry count:" + currentRetry);

            if (currentRetry >= retryCount || !IsTransient(error.name)) {
                console.log(error);
                return;
            }
            
            // await delay time and retry getData()
            setTimeout(() => {
                Retry();
            }, delay);
        }
    
}

function IsTransient(error){

    const transientError = "TimeoutError"; 
    if (error === transientError) {
       return true;
    }
    return false;
}



async function getData() {
    const browser = await puppeteer.launch();
    const page = await browser.newPage();
    await page.goto(url);
    const data = await page.content();
    await browser.close();
    processData(data);
}

function processData(data) {
    console.log("Processing Data...");
    const $ = cheerio.load(data);
    const prize = $(".number").first().text();
    fs.writeFileSync("output.txt", prize);
    console.log("Complete!");
}



