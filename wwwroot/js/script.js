// Function to log messages to the console box
function logToConsole(message) {
    const consoleBox = document.getElementById("consoleBox");
    const newLog = document.createElement("div");
    newLog.textContent = message; // Add plain text (no scaling or styles)
    consoleBox.appendChild(newLog);

    // Automatically scroll to the bottom of the console
    consoleBox.scrollTop = consoleBox.scrollHeight;
}

// Function to log messages to the console box and sync with localStorage
function logToConsole(message) {
    const consoleBox = document.getElementById("consoleBox");

    // Create a new div element for each log entry
    const newLog = document.createElement("div");
    newLog.textContent = message;
    consoleBox.appendChild(newLog);

    // Get current logs from localStorage
    let consoleLogs = JSON.parse(localStorage.getItem("consoleLogs")) || [];

    // Add the new log message to the array
    consoleLogs.push(message);

    // Update the console logs in localStorage
    localStorage.setItem("consoleLogs", JSON.stringify(consoleLogs));

    // Automatically scroll to the bottom of the console
    consoleBox.scrollTop = consoleBox.scrollHeight;
}

// Function to load the stored logs into the console
function loadConsoleLogs() {
    const consoleBox = document.getElementById("consoleBox");

    // Get logs from localStorage
    let consoleLogs = JSON.parse(localStorage.getItem("consoleLogs")) || [];

    // Add each log entry to the console box
    consoleLogs.forEach(log => {
        const newLog = document.createElement("div");
        newLog.textContent = log;
        consoleBox.appendChild(newLog);
    });

    // Automatically scroll to the bottom of the console
    consoleBox.scrollTop = consoleBox.scrollHeight;
}

// Call loadConsoleLogs on page load to display previous logs
window.onload = loadConsoleLogs;

// Function to log messages to the console box and sync with localStorage
function logToConsole(message) {
    const consoleBox = document.getElementById("consoleBox");

    // Create a new div element for each log entry
    const newLog = document.createElement("div");
    newLog.textContent = message;
    consoleBox.appendChild(newLog);

    // Get current logs from localStorage
    let consoleLogs = JSON.parse(localStorage.getItem("consoleLogs")) || [];

    // Add the new log message to the array
    consoleLogs.push(message);

    // Update the console logs in localStorage
    localStorage.setItem("consoleLogs", JSON.stringify(consoleLogs));

    // Automatically scroll to the bottom of the console
    consoleBox.scrollTop = consoleBox.scrollHeight;
}

// Function to load the stored logs into the console
function loadConsoleLogs() {
    const consoleBox = document.getElementById("consoleBox");

    // Get logs from localStorage
    let consoleLogs = JSON.parse(localStorage.getItem("consoleLogs")) || [];

    // Add each log entry to the console box
    consoleLogs.forEach(log => {
        const newLog = document.createElement("div");
        newLog.textContent = log;
        consoleBox.appendChild(newLog);
    });

    // Automatically scroll to the bottom of the console
    consoleBox.scrollTop = consoleBox.scrollHeight;
}
function sendKingdom(kingdom) {

    const jsonData = {
        api: "localtoken",
        command: "sendall " + kingdom
    };
    let destin = ''; // Initialize destin variable
    switch (kingdom) {
        case 'mush':
            destin = 'Mushroom Kingdom';
            break;
        case 'cap':
            destin = 'Cap Kingdom';
            break;
        case 'cascade':
            destin = 'Cascade Kingdom';
            break;
        case 'sand':
            destin = 'Sand Kingdom';
            break;
        case 'lake':
            destin = 'Lake Kingdom';
            break;
        case 'wooded':
            destin = 'Wooded Kingdom';
            break;
        case 'cloud':
            destin = 'Cloud Kingdom';
            break;
        case 'lost':
            destin = 'Lost Kingdom';
            break;
        case 'metro':
            destin = 'Metro Kingdom';
            break;
        case 'sea':
            destin = 'Seaside Kingdom';
            break;
        case 'snow':
            destin = 'Snow Kingdom';
            break;
        case 'lunch':
            destin = 'Luncheon Kingdom';
            break;
        case 'ruined':
            destin = 'Ruined Kingdom';
            break;
        case 'bowser':
            destin = 'Bowser\'s Kingdom';
            break;
        case 'moon':
            destin = 'Moon Kingdom';
            break;
        case 'dark':
            destin = 'Dark Side';
            break;
        case 'darker':
            destin = 'Darker Side';
            break;
        default:
            destin = 'Unknown Kingdom'; // Default case if none of the cases match
    }
    var resultString = "";
    console.log(localAddress + 'api/sendcommand')
    fetch(localAddress + 'api/sendcommand', {
        method: "POST", // Specify POST method
        headers: {
            "Content-Type": "application/json" // Indicate the body contains JSON
        },
        body: JSON.stringify(jsonData) // Convert JavaScript object to JSON string
    })
        .catch(error => {
            logToConsole('Error: ' + error);
        });
    resultString = 'Sending all Players to ' + destin
    logToConsole(resultString)
}


// Call loadConsoleLogs on page load to display previous logs
window.onload = loadConsoleLogs;

localStorage.getItem("consoleLogs")

