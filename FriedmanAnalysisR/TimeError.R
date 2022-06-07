#Set up
library(stats)
library(pgirmess)

mineTime<-read.delim("UNI/10. Semester/Stat/MineTime.dat", header = TRUE)
mineError<-read.delim("UNI/10. Semester/Stat/MineError.dat", header = TRUE)
decoTime<-read.delim("UNI/10. Semester/Stat/DecoTime.dat", header = TRUE)
decoError<-read.delim("UNI/10. Semester/Stat/DecoError.dat", header = TRUE)


#End set up________________________________________________________________

#Levels of experience with mine time
currentTable<-mineTime
none<-currentTable$None
vib<-currentTable$Vib
mag<-currentTable$Mag
mean(none)
mean(vib)
mean(mag)
sqrt(sum((none-mean(none))^2/(length(none))))
sqrt(sum((vib-mean(vib))^2/(length(vib))))
sqrt(sum((mag-mean(mag))^2/(length(mag))))
friedman.test(as.matrix(mineTime))
friedmanmc(as.matrix(mineTime))
#________________________________

#Levels of discomfort with mine errors
currentTable<-mineError
none<-currentTable$None
vib<-currentTable$Vib
mag<-currentTable$Mag
mean(none)
mean(vib)
mean(mag)
sqrt(sum((none-mean(none))^2/(length(none))))
sqrt(sum((vib-mean(vib))^2/(length(vib))))
sqrt(sum((mag-mean(mag))^2/(length(mag))))
friedman.test(as.matrix(mineError))
friedmanmc(as.matrix(mineError))
#________________________________

#Levels of immersion with decorator time
currentTable<-decoTime
none<-currentTable$None
vib<-currentTable$Vib
mag<-currentTable$Mag
mean(none)
mean(vib)
mean(mag)
sqrt(sum((none-mean(none))^2/(length(none))))
sqrt(sum((vib-mean(vib))^2/(length(vib))))
sqrt(sum((mag-mean(mag))^2/(length(mag))))
friedman.test(as.matrix(decoTime))
friedmanmc(as.matrix(decoTime))
#________________________________

#Levels of immersion with decorator error
currentTable<-decoError
none<-currentTable$None
vib<-currentTable$Vib
mag<-currentTable$Mag
mean(none)
mean(vib)
mean(mag)
sqrt(sum((none-mean(none))^2/(length(none))))
sqrt(sum((vib-mean(vib))^2/(length(vib))))
sqrt(sum((mag-mean(mag))^2/(length(mag))))
friedman.test(as.matrix(decoError))
friedmanmc(as.matrix(decoError))
#________________________________


