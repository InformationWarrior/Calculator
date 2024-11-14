const app = require('./app');

app.listen(8080, (error) => {
    if (!error) {
        console.log("Server running on port 8080...");
    } 
    else {
        console.error("Error starting server:", error);
    }
});
