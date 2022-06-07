from tracemalloc import stop
from turtle import distance
import requests
import random
from tkinter import *
from tkinter.ttk import *
import time
import threading

baseUrl = "http://192.168.0.34"

standardStim = 198
#comparisonStim = [5, 10, 20, 30, 40, 50, 60, 70, 80]
points = dict(
    {"168": [], 
    "175": [], 
    "183": [], 
    "190": [],
    "198": [], 
    "205": [], 
    "213": [], 
    "220": [],
    "228": []
}) 
keyPoints = list(points.keys())
random.shuffle(keyPoints)
testProgress = -1

dist = ""
marks = ""

stopValList = []

shouldAttract = True
goDown = False

shouldStop = False

def startReal():
    global shouldStop
    global stopval
    global goDown
    global standardStim
    global keyPoints
    global testProgress
    stopval = 0
    
    disableButs()
    #requests.get(url = baseUrl + "/hard/0/CW") #Kill the magnet first
    compVal = int(keyPoints[testProgress][1:-1])
    urlString = baseUrl + "/hard/" + str(compVal) + "/" + directionBox.get() + "/1000"
    urlLabel.configure(text=urlString)
    print(urlString)
    requests.get(url = urlString)
    time.sleep(1)
    #requests.get(url = baseUrl + "/hard/0/CW") #Kill the magnet first
    urlString = baseUrl + "/hard/" + str(standardStim) + "/" + directionBox.get() + "/1000"
    urlLabel.configure(text=urlString)
    print(urlString)
    requests.get(url = urlString)
    #time.sleep(1.5)
    #requests.get(url = baseUrl + "/hard/0/CW") #Kill the magnet on stop
    enableButs()

def start():
    global shouldStop
    shouldStop = False
    x = threading.Thread(target=startReal)
    x.start()

def yesClick():
    points[keyPoints[testProgress][1:-1]].append(1)
    #list(points.values())[testProgress].append(1)

def noClick():
    points[keyPoints[testProgress][1:-1]].append(0)
    #list(points.values())[testProgress].append(0)

def getAverage():
    global points
    print(points)
    newPoints = points
    for key in points:
        sum = 0
        for reply in points[key]:
            sum = sum + reply
        newPoints[key] = sum/len(points[key])
    return newPoints
    

def saveFile():
    global stopValList
    global marks
    global testProgress
    #print("LIST:")
    #print(stopValList)
    #print("PROG:")
    #print("THE AV:")
    #print(str(theAverage))
    f = open("psychoJND/part8.txt", "a")
    #f.write("Intensity difference: " + keyPoints[testProgress] + "\n")
    f.write("Direction:" + directionBox.get() + "\n")
    f.write("Results: " + str(getAverage()))
    #f.write("Stopping points:" + marks + "\n")
    #f.write("Average Threshold:" + str(theAverage) + "\n")
    f.write("\n")
    f.close()

def resetTest():
    global testProgress
    global keyPoints
    global testProgress
    if testProgress != -1: keyPoints[testProgress] = str(keyPoints[testProgress])[1:-1]
    testProgress = -1
    random.shuffle(keyPoints)
    rangeLabel.configure(text=str(keyPoints))


def continueFunc():
    global testProgress
    if testProgress != -1: keyPoints[testProgress] = str(keyPoints[testProgress])[1:-1]
    testProgress = testProgress + 1
    keyPoints[testProgress] = "{" + str(keyPoints[testProgress]) + "}"
    rangeLabel['text'] = str(keyPoints)


def enableButs():
    yesBut["state"] = "enable"
    noBut["state"] = "enable"
    resetBut["state"] = "enable"
    btnSave["state"] = "enable"

def disableButs():
    yesBut["state"] = "disabled"
    noBut["state"] = "disabled"
    resetBut["state"] = "disabled"
    btnSave["state"] = "disabled"

window = Tk()
window.title("Pilot Testing")
window.geometry('600x300')


directionLabel = Label(window, text="Direction:")
directionLabel.grid(column=0, row=0)
directionBox = Entry(window, width=5)
directionBox.grid(column=1, row=0)
directionBox.insert(-1, "CW")

rangeLabel = Label(window, text=str(keyPoints))
rangeLabel.grid(column=2, row=0)
nextBut = Button(window, text="➔", command=continueFunc)
nextBut.grid(column=3, row=0)

btnStart = Button(window, text="Play", command=start)
btnStart.grid(column=2, row=1)
urlLabel = Label(window, text=baseUrl)
urlLabel.grid(column=2, row=2)

questionLabel = Label(window, text="Was the first stimulus greater than the second?", width=14, wraplength=80)
questionLabel.grid(column=2, row=10)
yesBut = Button(window, text="Yes", command=yesClick)
yesBut.grid(column=1, row=11)
noBut = Button(window, text="No", command=noClick)
noBut.grid(column=3, row=11)

btnSave = Button(window, text="Save", command=saveFile)
btnSave.grid(column=2, row=15)

resetBut = Button(window, text="Reset", command=resetTest)
resetBut.grid(column=2, row=20)

requests.get(url = baseUrl + "/debug/")
window.mainloop()