# Deloitte_PasswordChecker
This is a program that evaluates a provided passwords strength, provides feedback for increasing the complexity of a password and also checks to see whether the password has been breached or not.

## Requirements

This program has been implemented in C# using  **[.Net Core 2.2](https://dotnet.microsoft.com/download)**.

```
Both the tests and the application have been developed through .Net Core 2.2

In order to run the application please ensure the .Net Core 2.2 SDK is installed for Visual Studio.
```

## Calculating Password Strength

The password strength is calculated through finding the password entropy in bits:
```
Depending on the password and characters a the "Pool of Characters Possible" will need to be determined:
        lowercase = 26,
        lowerAndUpperCase = 52,
        alphanumeric = 36,
        alphanumericAndUpperCase = 62
        
After determining the pool of characters possible, the following formula will be applied to calculate 
the password entropy:
        entropy = log(base 2)R^L
        
        R = Pool of Unique Characters
        L = Length of Password
        
When the entropy is determined, this number can be used to advise users how strong the entered password
is through the following guidelines:
        < 28 bits = Very Weak;
        28 - 35 bits = Weak;
        36 - 59 bits = Reasonable;
        60 - 127 bits = Strong;
        128+ bits = Very Strong;
        
Apart from that, the user will be provided with additional guidelines to increase the complexity of their password.
```

## Number of times the password has been leaked

After calculating the strength, the program checks to see whether the password has been leaked and how
many times by querying the [HaveIBeenPwned V2 API](https://haveibeenpwned.com/API/v2#PwnedPasswords):
```
1. The password is hashed using the SHA1 hashing algorithm

2. The first 5 characters of the hashed password is extracted as the prefix (to comply with the 
k-Anonymity model) and the remaining characters are stored as the suffix

3. Using the prefix, a GET request is sent to the API and all results with that prefix are returned 
(381-584 results)

4. The results from the GET request are stored in a List and the passwords suffix is queried using LINQ
        - The result is formatted as follows: 0136E006E24E7D152139815FB0FC6A50B15:2

5. The second part of the returned string determines the number of times the password has been breached and
this is relayed to the user on the console application
```
