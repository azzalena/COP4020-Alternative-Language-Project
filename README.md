# COP4020-Alternative-Language-Project
This is the alternative language project for COP4020 with the use of C# 12. C is a fundamental language for most programmers, so adding another C under my belt is something I think is the most beneficial to me.

C# supports everything objected-oritnted such as encapsulation. It allows for creation of classes, objects, interfaces, inheritance, and other OOP concepts. C# methods for file ingestion include reading and writing files usiing classes such as 'FileStream'. C# supports traditional statements such as 'if' and 'switch' statement. For assignment statements, C# follows the syntax variable = value. C# in terms of loops supports various types such as 'for' and while'. It could be used to loop through arrays as well. C# supports defining and calling methods and functions as well as having parameters and return types. For testing, C# has NUnit, MStest, etc. For exeception handling, C# has comprehensive support such as 'try', 'catch', 'finally', and 'throw'.

As for what libraries I have used, these are the ones and the reasons:

System.IO: The System.IO namespace provides classes for performing input and output operations in C#. It includes classes for reading from and writing to files, working with streams, directories, and paths. I often use this library when working with file ingestion, manipulation, and management tasks. For example, I might use classes like File, Directory, FileStream, StreamReader, and StreamWriter to read data from files, write data to files, or manipulate file paths.

System.Collections.Generic: The System.Collections.Generic namespace provides generic collection classes in C#. These collections offer type safety, performance, and efficiency compared to non-generic collections. I frequently use classes like List<T>, Dictionary<TKey, TValue>, HashSet<T>, etc., for storing and manipulating collections of data. These collections are versatile and efficient, making them suitable for various data storage and retrieval needs in C# applications.

System.Linq: The System.Linq namespace provides language-integrated query capabilities in C# using LINQ (Language Integrated Query). LINQ enables developers to query and manipulate data from different data sources (such as collections, arrays, databases) using a consistent syntax. I often use LINQ to perform operations like filtering, sorting, grouping, and aggregating data in a concise and readable manner. It offers powerful features that streamline data manipulation tasks and improve code readability and maintainability.

I chose these libraries because they are fundamental to many C# development tasks and provide efficient and convenient solutions for common programming challenges. They offer robust functionality, good performance, and are well-integrated with the C# language, making them ideal choices for a wide range of applications.\

As for the questions asked, and it is printed in the code if ran:

What company (oem) has the highest average weight of the phone body?
Benefon

Was there any phones that were announced in one year and released in another? What are they? Give me the oem and models.
There were a lot, but some that were included are Huawei with that Mate 30 model and ZTE with their blade model.

How many phones have only one feature sensor?
1001

What year had the most phones launched in any year later than 1999? 
2019

Extra note: the update has the clean out data printed with missing values replaced and incdicated.
