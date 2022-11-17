Set-Location $env:USERPROFILE\Projects\ExpectedValueOfLottery\

# Get the current prize by crawling powerball.com
# And then save the result string in output.txt
node .\crawler.js

# Get the result string from output.txt
# And then create index.html
Set-Location .\UpdatePage
dotnet run 

# Take a look at index.html
Start-Process ..\index.html

# Check if everything's fine...
Pause