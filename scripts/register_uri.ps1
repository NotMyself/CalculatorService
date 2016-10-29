$userName = Read-Host -Prompt 'Input User Name (notmyself for local OR domain\notmyself for AD)' 
 
 netsh http add urlacl url=http://+:8080/calc user="$userName"