from tracemalloc import stop
from turtle import distance
import requests
import random
from tkinter import *
from tkinter.ttk import *
import time
import threading

URL = "http://192.168.53.34"
startVal = 60
endVal = 255
raiseVal = 8
progress = 0

dist = ""

marks = ""

stopValList = []

shouldAttract = True
goDown = False

shouldStop = False

def getRandom():
    global dist
    dist = str(random.randrange(int(txtRange1.get(), 10), int(txtRange2.get(), 10)+1))
    rangeLabel.configure(text=dist)

def startReal():
    global shouldStop
    global stopval
    global goDown
    global startVal
    global endVal
    global raiseVal
    stopval = 0

    if not goDown:
        firstVal = startVal
        secondVal = endVal + 1
        incrementVal = raiseVal
    else:
        firstVal = endVal
        secondVal = startVal
        incrementVal = -raiseVal
        
    for x in range(firstVal, secondVal, incrementVal):
        requests.get(url = "http://192.168.0.34/hard/0/CW") #Kill the magnet first
        if shouldStop:
            break
        urlString = "http://192.168.0.34/hard/" + str(x) + "/" + directionBox.get()
        urlLabel.configure(text=urlString)
        requests.get(url = urlString)
        stopval = x
        time.sleep((random.randrange(500,1200)/1000.0))

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
    global dist
    print("LIST:")
    print(stopValList)
    print("PROG:")
    print(progress)
    theAverage = getAverage(stopValList, progress)
    print("THE AV:")
    print(str(theAverage))
    f = open("psychoForce/part8.txt", "a")
    f.write("Distance: " + dist + "\n")
    f.write("Direction:" + directionBox.get() + "\n")
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

window = Tk()
window.title("Pilot Testing")
window.geometry('600x300')

txtRange1 = Entry(window, width=5)
txtRange1.grid(column=0, row=0)
txtRange1.insert(-1, '0')
rangeLabel = Label(window, text="0")
rangeLabel.grid(column=1, row=0)
txtRange2 = Entry(window, width=5)
txtRange2.grid(column=2, row=0)
txtRange2.insert(-1, '4')
btnRand = Button(window, text="Get Random", command=getRandom)
btnRand.grid(column=3, row=0)

btnStart = Button(window, text="Start", command=start)
btnStart.grid(column=2, row=1)
urlLabel = Label(window, text=URL)
urlLabel.grid(column=2, row=2)

btnMark = Button(window, text="Mark!", command=mark)
btnMark.grid(column=2, row=3)
progressLabel = Label(window, text="Progress: " + str(progress))
progressLabel.grid(column=2, row=4)

directionLabel = Label(window, text="Direction:")
directionLabel.grid(column=0, row=6)
directionBox = Entry(window, width=5)
directionBox.grid(column=1, row=6)
directionBox.insert(-1, "CW")

btnSave = Button(window, text="Save", command=saveFile)
btnSave.grid(column=2, row=9)

btnReset = Button(window, text="Reset!", command=resetProgress)
btnReset.grid(column=3, row=9)


window.mainloop()