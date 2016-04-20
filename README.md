# Occurrence Counter

Occurrence Counter is a .NET application which performs calculation of words' occurrence in a provided text. A user can insert it as a free text or select a text file to process. 

It consists of two modules:

* **OccurrenceCounter** - class library with the main logic of the application 
* **OccurrenceCounterClient** - WPF client application 

Each of modules has its own test project. 

Thanks to dependency injection and Prism library, the client application can be easily extended with new functionalities.
Design of the OccurrenceCounter library with well defined interface makes it reusable by any other client application.

In order to calculate the number of occurrence I used the MapReduce-like model. The solution was designed to process data on a single machine but in case the input data does not fit single machine memory it could be reworked to MapReduce implementation.

The additional goal of this solution is to demonstrate development skills and usage of the following technologies: 

* .NET 4.5.2
* C#
* WPF
* MVVM
* Prism 6
* Unity
* async and await
* PLINQ
* NUnit
* Moq

MvvmDialogs v1.2.16 is used as file explorer.

