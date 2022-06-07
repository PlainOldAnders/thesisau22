#Set up
library(stats)
library(pgirmess)

experienceMine<-read.delim("UNI/10. Semester/Stat/ExperienceMine.dat", header = TRUE)
discomfortMine<-read.delim("UNI/10. Semester/Stat/DiscomfortMine.dat", header = TRUE)
immersionMine<-read.delim("UNI/10. Semester/Stat/ImmersionMine.dat", header = TRUE)

experienceDeco<-read.delim("UNI/10. Semester/Stat/ExperienceDeco.dat", header = TRUE)
discomfortDeco<-read.delim("UNI/10. Semester/Stat/DiscomfortDeco.dat", header = TRUE)
immersionDeco<-read.delim("UNI/10. Semester/Stat/ImmersionDeco.dat", header = TRUE)
#End set up________________________________________________________________

#Levels of experience with mine
currentTable<-experienceMine
none<-currentTable$None
vib<-currentTable$Vib
mag<-currentTable$Mag
mean(none)
mean(vib)
mean(mag)
sqrt(sum((none-mean(none))^2/(length(none))))
sqrt(sum((vib-mean(vib))^2/(length(vib))))
sqrt(sum((mag-mean(mag))^2/(length(mag))))
friedman.test(as.matrix(experienceMine))
friedmanmc(as.matrix(experienceMine))
#________________________________

#Levels of discomfort with mine
currentTable<-discomfortMine
none<-currentTable$None
vib<-currentTable$Vib
mag<-currentTable$Mag
mean(none)
mean(vib)
mean(mag)
sqrt(sum((none-mean(none))^2/(length(none))))
sqrt(sum((vib-mean(vib))^2/(length(vib))))
sqrt(sum((mag-mean(mag))^2/(length(mag))))
friedman.test(as.matrix(discomfortMine))
friedmanmc(as.matrix(discomfortMine))
#________________________________

#Levels of immersion with mine
currentTable<-immersionMine
none<-currentTable$None
vib<-currentTable$Vib
mag<-currentTable$Mag
mean(none)
mean(vib)
mean(mag)
sqrt(sum((none-mean(none))^2/(length(none))))
sqrt(sum((vib-mean(vib))^2/(length(vib))))
sqrt(sum((mag-mean(mag))^2/(length(mag))))
friedman.test(as.matrix(immersionMine))
friedmanmc(as.matrix(immersionMine))
#________________________________
#________________________________
#________________________________

#Levels of experience with decorator
currentTable<-experienceDeco
none<-currentTable$None
vib<-currentTable$Vib
mag<-currentTable$Mag
mean(none)
mean(vib)
mean(mag)
sqrt(sum((none-mean(none))^2/(length(none))))
sqrt(sum((vib-mean(vib))^2/(length(vib))))
sqrt(sum((mag-mean(mag))^2/(length(mag))))
friedman.test(as.matrix(experienceDeco))
friedmanmc(as.matrix(experienceDeco))
#________________________________

#Levels of discomfort with decorator
currentTable<-discomfortDeco
none<-currentTable$None
vib<-currentTable$Vib
mag<-currentTable$Mag
mean(none)
mean(vib)
mean(mag)
sqrt(sum((none-mean(none))^2/(length(none))))
sqrt(sum((vib-mean(vib))^2/(length(vib))))
sqrt(sum((mag-mean(mag))^2/(length(mag))))
friedman.test(as.matrix(discomfortDeco))
friedmanmc(as.matrix(discomfortDeco))
#________________________________

#Levels of immersion with decorator
currentTable<-immersionDeco
none<-currentTable$None
vib<-currentTable$Vib
mag<-currentTable$Mag
mean(none)
mean(vib)
mean(mag)
sqrt(sum((none-mean(none))^2/(length(none))))
sqrt(sum((vib-mean(vib))^2/(length(vib))))
sqrt(sum((mag-mean(mag))^2/(length(mag))))
friedman.test(as.matrix(immersionDeco))
friedmanmc(as.matrix(immersionDeco))
#________________________________


