#Set up
library(stats)
library(pgirmess)

experience<-read.delim("UNI/10. Semester/Stat/Experience.dat", header = TRUE)
discomfort<-read.delim("UNI/10. Semester/Stat/Discomfort.dat", header = TRUE)
immersion<-read.delim("UNI/10. Semester/Stat/Immersion.dat", header = TRUE)
#End set up________________________________________________________________

#Levels of experience
none<-experience$None
vib<-experience$Vib
mag<-experience$Mag
mean(none)
mean(vib)
mean(mag)
sqrt(sum((none-mean(none))^2/(length(none))))
sqrt(sum((vib-mean(vib))^2/(length(vib))))
sqrt(sum((mag-mean(mag))^2/(length(mag))))
friedman.test(as.matrix(experience))
friedmanmc(as.matrix(experience))
#________________________________

#Levels of discomfort
none<-discomfort$None
vib<-discomfort$Vib
mag<-discomfort$Mag
mean(none)
mean(vib)
mean(mag)
sqrt(sum((none-mean(none))^2/(length(none))))
sqrt(sum((vib-mean(vib))^2/(length(vib))))
sqrt(sum((mag-mean(mag))^2/(length(mag))))
friedman.test(as.matrix(discomfort))
friedmanmc(as.matrix(discomfort))
#________________________________

#Levels of immersion
none<-immersion$None
vib<-immersion$Vib
mag<-immersion$Mag
mean(none)
mean(vib)
mean(mag)
sqrt(sum((none-mean(none))^2/(length(none))))
sqrt(sum((vib-mean(vib))^2/(length(vib))))
sqrt(sum((mag-mean(mag))^2/(length(mag))))
friedman.test(as.matrix(immersion))
friedmanmc(as.matrix(immersion))
#________________________________

