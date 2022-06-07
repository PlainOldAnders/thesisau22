from tracemalloc import stop
from turtle import distance
import requests
import random
from tkinter import *
from tkinter.ttk import *
import time
import threading

baseUrl = "http://192.168.0.34"
startVal = 0
endVal = 255
raiseVal = 5
progress = 0

#(0,0)(-1,0)(2,0)(3,0)(0,-4)(-5,0)(0,6)(7,0)(0,-9)(-11,0)(0,-13)

points = dict(
    {"0": [0, 130], 
    "1": [0, 130], 
    "2": [5, 140], 
    "3": [5, 150],
    "4": [10, 160], 
    "5": [20, 200], 
    "6": [20, 200], 
    "7": [70, 250],
    "9": [100, 256],
    "11": [120, 256], 
    "13": [200, 256],
}) 
keyPoints =  list(points.keys())
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
    global startVal
    global endVal
    global raiseVal
    global points
    global keyPoints
    global testProgress
    stopval = 0

    if not goDown:
        #firstVal = startVal
        firstVal = points.get(keyPoints[testProgress][1:-1])[0]
        #secondVal = endVal + 1
        secondVal = points.get(keyPoints[testProgress][1:-1])[1]
        incrementVal = raiseVal
    else:
        #firstVal = endVal
        firstVal = points.get(keyPoints[testProgress][1:-1])[1]
        #secondVal = startVal
        secondVal = points.get(keyPoints[testProgress][1:-1])[0]
        incrementVal = -raiseVal
        
    for x in range(firstVal, secondVal, incrementVal):
        requests.get(url = "http://192.168.0.34/hard/0/CW") #Kill the magnet first
        if shouldStop:
            break
        val = random.randrange(40, 80, 10)
        urlString = "http://192.168.0.34/vibrate/" + str(x) + "/10/" +  str(val) + "/" + "both"
        urlLabel.configure(text=urlString)
        requests.get(url = urlString)
        stopval = x
        #time.sleep(2)

def start():
    global shouldStop
    shouldStop = False
    x = threading.Thread(target=startReal)
    x.start()

def mark():
    global marks
    global stopValList
    global goDown
    stopValList.append(stopval)
    if goDown:
        marks+= str(stopval) + "v"
    else:
        marks+= str(stopval) + "^"
    print(stopValList)
    global shouldStop
    
    global progress
    progress+=1
    progressLabel['text'] = "Progress: " + str(progress)
    goDown = not goDown
    shouldStop = True
    getAverage(stopValList, progress)

def getAverage(list, prog):
    finalVal = 0
    for x in list:
        finalVal+= x
    return finalVal/prog

def saveFile():
    global stopValList
    global progress
    global marks
    global testProgress
    global keyPoints
    print("LIST:")
    print(stopValList)
    print("PROG:")
    print(progress)
    theAverage = getAverage(stopValList, progress)
    print("THE AV:")
    print(str(theAverage))
    f = open("psychoVib/part8.txt", "a")
    f.write("Distance: " + keyPoints[testProgress] + "\n")
    f.write("Direction:" + "both" + "\n")
    f.write("No. of runs: " + str(progress)+"\n")
    f.write("Stopping points:" + marks + "\n")
    f.write("Average Threshold:" + str(theAverage) + "\n")
    f.write("\n")
    f.close()

def resetProgress():
    global progress
    global marks
    global stopValList
    marks = ""
    stopValList = []
    progress = 0
    progressLabel['text'] = "Progress: " + str(progress)

def continueFunc():
    global testProgress
    if testProgress != -1: keyPoints[testProgress] = str(keyPoints[testProgress])[1:-1]
    testProgress = testProgress + 1
    keyPoints[testProgress] = "{" + str(keyPoints[testProgress]) + "}"
    rangeLabel['text'] = str(keyPoints)

window = Tk()
window.title("Pilot Testing")
window.geometry('600x300')


rangeLabel = Label(window, text=str(keyPoints))
rangeLabel.grid(column=2, row=0)
nextBut = Button(window, text="➔", command=continueFunc)
nextBut.grid(column=3, row=0)

btnStart = Button(window, text="Start", command=start)
btnStart.grid(column=2, row=1)
urlLabel = Label(window, text=baseUrl)
urlLabel.grid(column=2, row=2)

btnMark = Button(window, text="Mark!", command=mark)
btnMark.grid(column=2, row=3)
progressLabel = Label(window, text="Progress: " + str(progress))
progressLabel.grid(column=2, row=4)

btnSave = Button(window, text="Save", command=saveFile)
btnSave.grid(column=2, row=9)

btnReset = Button(window, text="Reset!", command=resetProgress)
btnReset.grid(column=3, row=9)


window.mainloop()