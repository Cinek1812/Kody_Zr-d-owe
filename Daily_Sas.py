import re
text = open('G:\****', 'r').read()
temporaryText = text.replace("<", "\n<")
pattern = r'<STATUS1>.*'
file = open("D:\DWH\workbench_sas.txt", 'w')
file.write(temporaryText)
file.close()
 
rightFile = open("D:\DWH\****", 'w')
#print text2
list = []
readFile = open('D:\workbench_sas.txt', 'r').readlines()

for line in readFile:
    matcher = re.search(pattern,line)
    if matcher: #if we found sth
        wholeText = matcher.group()
        properText = number[9:]
        list.append(wholeText)
for item in list:
    if item == "Error":
        e.write("Error")
        break
rightFile.close()
