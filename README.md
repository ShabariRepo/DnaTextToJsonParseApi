# DnaTextToJsonParseApi
test to text whether Text passed in with URI using REST call can be parsed to JSON OBJECT

Parse Text to JSON:
1. text will be passed in with the URI parameters with the '?text=' param (/Controller/Api)
2. The controller will then use the interface or method from ParseTextToJson model (ParseText() method)(/Dto)
3. Operators will be implicitly converted to JSON objects using the DnaLog and Operator models
    - 3a. There are 4 models and the main model being LogDnaDto (/Dto)
    - 3b. Any chages made will be only to the ParseTextToJson class unless model needs to be expanded
4. DTO objects are used to not interfere with the original models under /models
5. If ;and; not specified, then will default to OR
    - 5a. <400;>500; will default to <400;or;>500;
6. Not supported yet :( :
    - Multiple String inputs within ( )
    - ! operator will be parsed as its own object
    - Int conversion of the len(..) value
        - to fix this if it needs to be an int, may need to extract into a partial class or struct
        - if not the int will always show even if not set in the JSON object
    - < > operators work but will need to test for <= or >= operators (if needed fix coming soon :)..)
    
Example URI:   "http://localhost:11974/api/dna?text="test data";or;>len(9);"



Sample images:
![image](https://user-images.githubusercontent.com/10635357/31749227-af01a53e-b446-11e7-81bb-eaca911b0c53.png)

![image](https://user-images.githubusercontent.com/10635357/31749249-cb564000-b446-11e7-9c4d-2cd0e792637d.png)

![image](https://user-images.githubusercontent.com/10635357/31749280-f871776c-b446-11e7-88b4-7dc9dd36da44.png)

![image](https://user-images.githubusercontent.com/10635357/31749298-267cafe6-b447-11e7-8066-d672c6fd2cab.png)
