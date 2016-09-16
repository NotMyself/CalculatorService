$userName = Read-Host -Prompt 'Input User Name' 
 
 netsh http add urlacl url=http://+:8080/calc user="ESD1\$userName"