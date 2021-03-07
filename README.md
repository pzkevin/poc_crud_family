# poc_crud_family
POC of a class that enables to abstract the DB functionalities in C#
To use by my alumni in my POO course. UTHermosillo

## _As in we did in class, all acctions works with a 'products' table_
## _Remember to check the Class diagram that we did on class_
---

## 1. clone the repo in a folder of your choice

```sh
git clone https://github.com/alonsolopezr/poc_crud_family.git
```
### 2. create a brnach of your own
```sh
git checkout -b mi_rama_APELLIDO
```

## 3. Modify and play with the code
### use the DataCollection for DML and SearchCollection for SELECT sentences as your choice.
### Put special atention on the logic we employ to "parse" the collections and generate for each of the METHODS: 
  - create(data: List<DataCollection>): bool
  - update(data: List<DataCollection>, id:int): bool
  - delete(id:int): bool
  - seach(search: List<SearchCollection>): List<object>
  - index(): List<object>
---
👁 Atención 👁 
# ok, now, make this works wih the class Persona, Admin, Cashier, in the 'Users' package
 
## Commit and push your work in your branch
```sh
git add .
git commit -s -m "tu avance "
git push origin mi_rama_APELLIDO
```

#Done 👌🏻
