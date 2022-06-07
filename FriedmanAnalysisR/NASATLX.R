#Set up
library(stats)
library(pgirmess)

mental<-read.delim("UNI/10. Semester/Stat/tlx/mental.dat", header = TRUE)
physical<-read.delim("UNI/10. Semester/Stat/tlx/physical.dat", header = TRUE)
temporal<-read.delim("UNI/10. Semester/Stat/tlx/temporal.dat", header = TRUE)
performance<-read.delim("UNI/10. Semester/Stat/tlx/performance.dat", header = TRUE)
effort<-read.delim("UNI/10. Semester/Stat/tlx/effort.dat", header = TRUE)
frustration<-read.delim("UNI/10. Semester/Stat/tlx/frustration.dat", header = TRUE)
#End set up________________________________________________________________

#Levels of mental
currentTable<-mental
vis<-currentTable$Vis
vib<-currentTable$Vib
mag<-currentTable$Mag
mean(vis)
mean(vib)
mean(mag)
sqrt(sum((vis-mean(vis))^2/(length(vis))))
sqrt(sum((vib-mean(vib))^2/(length(vib))))
sqrt(sum((mag-mean(mag))^2/(length(mag))))
friedman.test(as.matrix(mental))
friedmanmc(as.matrix(mental))
#________________________________

#Levels of physical
currentTable<-physical
vis<-currentTable$Vis
vib<-currentTable$Vib
mag<-currentTable$Mag
mean(vis)
mean(vib)
mean(mag)
sqrt(sum((vis-mean(vis))^2/(length(vis))))
sqrt(sum((vib-mean(vib))^2/(length(vib))))
sqrt(sum((mag-mean(mag))^2/(length(mag))))
friedman.test(as.matrix(physical))
friedmanmc(as.matrix(physical))
#________________________________

#Levels of temporal
currentTable<-temporal
vis<-currentTable$Vis
vib<-currentTable$Vib
mag<-currentTable$Mag
mean(vis)
mean(vib)
mean(mag)
sqrt(sum((vis-mean(vis))^2/(length(vis))))
sqrt(sum((vib-mean(vib))^2/(length(vib))))
sqrt(sum((mag-mean(mag))^2/(length(mag))))
friedman.test(as.matrix(temporal))
friedmanmc(as.matrix(temporal))
#________________________________


#Levels of performance
currentTable<-performance
vis<-currentTable$Vis
vib<-currentTable$Vib
mag<-currentTable$Mag
mean(vis)
mean(vib)
mean(mag)
sqrt(sum((vis-mean(vis))^2/(length(vis))))
sqrt(sum((vib-mean(vib))^2/(length(vib))))
sqrt(sum((mag-mean(mag))^2/(length(mag))))
friedman.test(as.matrix(performance))
friedmanmc(as.matrix(performance))
#________________________________

#Levels of effort
currentTable<-effort
vis<-currentTable$Vis
vib<-currentTable$Vib
mag<-currentTable$Mag
mean(vis)
mean(vib)
mean(mag)
sqrt(sum((vis-mean(vis))^2/(length(vis))))
sqrt(sum((vib-mean(vib))^2/(length(vib))))
sqrt(sum((mag-mean(mag))^2/(length(mag))))
friedman.test(as.matrix(effort))
friedmanmc(as.matrix(effort))
#________________________________

#Levels of frustration
currentTable<-frustration
vis<-currentTable$Vis
vib<-currentTable$Vib
mag<-currentTable$Mag
mean(vis)
mean(vib)
mean(mag)
sqrt(sum((vis-mean(vis))^2/(length(vis))))
sqrt(sum((vib-mean(vib))^2/(length(vib))))
sqrt(sum((mag-mean(mag))^2/(length(mag))))
friedman.test(as.matrix(frustration))
friedmanmc(as.matrix(frustration))
#________________________________


