# Random numbers
Create a NET Core webapi application 2.1 or higher for numerical calculation assessments. All random numbers Use
must be generated with RNGCryptoServiceProvider, do not use System.Random. If you use a reusable classlibrary, create
it in NET Standard 2.0 or higher. Create also unit tests to validate if actions are working properly.
## CalculationController
This controller will have 4 methods:
* **GET /api/calculation/sums**, generates a 16 sets of data, each set will hold 3 numbers, a result of the sum of the 3
numbers and a boolean to tell if sum result is valid.
* **GET /api/calculation/subtractions**, generates a 16 sets of data, each set will hold 2 numbers, a result of the
subtraction of the 3 numbers and a boolean to tell if subtraction result is valid.
* **GET /api/calculation/multiplications**, generates a 16 sets of data, each set will hold 2 numbers, a result of the
multiplication of the 3 numbers and a boolean to tell if multiplication result is valid.
* **GET /api/calculation/divisions**, generates a 16 sets of data, each set will hold 2 numbers, a result of the divisions
of the 3 numbers and a boolean to tell if multiplication result is valid.
The API will randomly print a correct or wrong Sum for those 3 numbers and a Boolean to indicate if the sum is valid or
not.
```javascript
GET /api/calculation/sums
JSON to be returned:      
 [
  {
    "Set": {
      "Numbers": [13, 45, 66]
      "Result": 134,
      "IsValidResult": false
    }
  },
  {
    "Set": {
      "Numbers": [95, 12, 74]
      "Result": 181,
      "IsValidResult": true
    }
  },
  …
 ]
```
```javascript
GET /api/calculation/subtractions
JSON to be returned:
[
 {
   “Set”: {
     “Numbers”: [789, 654],
     “Result”: 135,
     “IsValidResult”: true
   }
 },
 {
   “Set”: {
     “Numbers”: [789, 654],
     “Result”: 502,
     “IsValidResult”: false
   }
 },
 …
]
```
```javascript
GET /api/calculation/multiplications
JSON to be returned:
[
  {
   “Set”: {
     “Number1”: 8954,
     “Number2”: 6,
     “Result”: 53722,
     “IsValidResult”: false
   }
 },
 {
   “Set”: {
     “Number1”: 3576,
     “Number2”:5,
     “Result”: 17880,
     “IsValidResult”: true
   }
 },
 …
]
```
```javascript
GET /api/calculation/divisions
JSON to be returned:
[
 {
   “Set”: {
     “Number1”: 324,
     “Number2”: 9,
     “Result”: 36,
     “IsValidResult”: true
   }
 },
 {
   “Set”: {
     “Number1”: 1539,
     “Number2”: 3,
     “Result”: 516,
     “IsValidResult”: false
   }
 },
 …
]
```
For each method, the random numbers must have within a specific range / limits:

GET /api/calculation/sums
* Number1 must have two digits: from 10 to 99
* Number2 must have two digits: from 10 to 99
* Number3 must have two digits: from 10 to 99

GET /api/calculation/subtractions
* Number1 must have three digits: from 100 to 999
* Number2 must have three digits and must be smaller than Number1

GET /api/calculation/multiplications
* Number1 must have four digits: from 100 to 999
* Number2 must have 1 digit: from 2 to 9

GET /api/calculation/divisions
* Number1 must have three or four digits: from 2 to 9

## MultipleCalculationController
The controller will have 1 method:
* **GET /api/multiplecalculation/expression**, generates a mix of 25 arithmetic expressions.
API will generate 4 numbers with random arithmetic operations symbols (+ or -).
Each of the four numbers must be between 1 and 99.
```javascipt
GET /api/multiplecalculation/expression
JSON to be returned:
[
 {
   “Expression”: “85 – 27 + 7 - 3”,
   “Result”: 62
 },
 {
   “Expression”: “9 + 5 + 16 - 7”,
   “Result”: 23
 },
 …
]
```
## SeriesController
The controller will have 2 methods:
* **GET /api/series/duplicates**, generates a JSON with 20 sets of data. Each set will contain 4 rows and each row will
show 4 random numbers from 0 to 9. The method will also show a set which column index and row index has
duplicate numbers
Each generated number must be between 1 and 999.
```javascript
GET /api/series/duplicates
JSON to be returned:
[
 {
   “Set”: [[1, 2, 3, 4],
           [2, 3, 3, 5],
           [6, 7, 8, 9],
           [0, 8, 2, 7]],
   “ColumnWithDuplicate”: [2],
   “RowWithDuplicate”: [1]
 },
 {
   “Set”: [[2, 5, 8, 9],
           [5, 8, 9, 6],
           [3, 1, 5, 7],
           [0, 1, 2, 7]],
   “ColumnWithDuplicate”: [1, 3]
   “RowWithDuplicate”: []
 },
 {
   “Set”: [[4, 1, 0, 0],
           [4, 5, 8, 9],
           [9, 6, 2, 5],
           [9, 6, 4, 7]],
   “ColumnWithDuplicate”: [0, 1],
   “RowWithDuplicate”: [0]
 },
 …
]
```
* **GET /api/series/search**, generates a JSON with an array of 625 numbers. The method will choose 4 random
numbers to be found in this array.
Then there will be a property to inform the position (index) of these random numbers at any part of the array.
Each number of the array must be between 0 and 9.
```javascript
GET /api/series/search
JSON to be returned:
 {
   “NumbersToFind”: [2, 5, 7, 9],
   “Array”: [
     3,4,5,6,4,7,8,9,2,3,5,4,6,7,7,7,4,6,3,7,8,8,1,3,4,
     2,3,5,8,3,2,5,5,7...
   ],
   “Result”: {
     {“PositionsOfNumber1”: [8, 25, 30, ...] },
     {“PositionsOfNumber2”: [2, 10, 27, 31, 32, ...] },
     {“PositionsOfNumber3”: [5, 13, 14, 15, 19, 33...] },
     {“PositionsOfNumber4”: [7,] }
   }
 }
 …
```
* **GET /api/series/exactsearch**, generates a JSON with an array of 625 numbers. The method will choose two pairs
of 4 random numbers to be found in the array in the same order they were generated.
If we have the pair 1 and 2, the result will hold positions of array where this pair appears.
Each number of the array must be between 0 and 9.
```javascript
GET /api/series/exactsearch
JSON to be returned:
 {
   “ExactPairsToFind”: [
     “Pair1”: [1, 2],
     “Pair2”: [5, 9]
  ],
   “Array”: [
      5,9,3,2,1,5,8,1,2,5,5,7,8,9,0,1,3,0,8,6,5,4,6,7,8...
   ],
   “Result”: {
     {“PositionsOfPair1”: [7, ...] },
     {“PositionsOfPair2”: [0, ...] } 
   }
 }
 …
 ```

