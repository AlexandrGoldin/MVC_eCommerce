# MVC_eCommerce
### Online store emulator in ASP.NET MVC C#
+ Sql script for this project here https://github.com/AlexandrGoldin/MVC_eCommerce_SqlScript
#### Stack:
+ ASP.NET MVC 5
+ Generic Repository & Unit Of Work Pattern
+ Forms Authentication
+ AutoMapper                                                        
+  PagedList/PagedList.Mvc
+ Mvc.Razor
+ Start Bootstrap SB Admin(template for admin area) 
+ Newtonsoft.Json
+ LINQ
+ Responsive design
+ Jquery
+ Bootstrap/HTML/CSS
+ T SQL/MS SQL Server
+ Entity Framework
+ Data Transfer Object
______________
+ На главная странице(Home/Index) в меню расположен кликабельный логотип, иконки заказов, корзина покупок, вход для админа. В подменю находится кнопка "Каталог" с выпадающим списком по категориям продуктов и форма поиска по продукту или категории. Ниже расположена Multi Column Image Carousel Bootstrap, carousel-inner содержит три items и в каждом item по три кликабельных изображения с caption ведущих к категориям или подгруппам продуктов. Ниже находится кнопка с выпадающим списком для сортировки по цене. Продукты, отображаемые на стр Home/Index должны иметь св-во IsDelete - false. Карта продукта включает badge(new/old), кликабельное изображение и название продукта, цену и наличие продукта. Иконка корзины ведет на Home/ProductDetails. Для пагинации применяется PagedList.
+ Справа показан Responsive design главнаой страницы на дисплее col-xs.

![home index1_3new](https://user-images.githubusercontent.com/50864552/213481442-19259fae-3cd5-4279-8db8-f2ba3ca59897.png)
__________________________
+ Кнопка Add to cart на странице продукта ведет на страцицу Cart Details для оформлениия заказа.
+ Справа показан Responsive design страницы на дисплее col-xs.

![home productDetails1_2new](https://user-images.githubusercontent.com/50864552/213481903-27ca88e8-6c01-4d62-b701-9ada3b8cf0d8.png)
______________
+ На страцице Cart Details выводится изображения и описание выбранных продуктов. Доступно изменение количества продукта или его удаление. Кнопка Order ведёт на стр Create Order c инф-ей о продуктах, итоговой ценой и формой для ввода данных о покупателе. После заполнения формы и клика по Order откроется стр Current orders.

![cart_order checoutCart_createOrder1](https://user-images.githubusercontent.com/50864552/213482189-3f31556a-ccb7-4eb1-8fbc-6848cbb5dadc.png)
________
+ Стр Current orders отображает изображения заказанных продуктов и стоимость заказа. Количество заказов за текущую сессию отображается в основном меню рядом с иконкой Заказы. Иконке Заказы в основном меню ведет на стр Current orders. Кнопка All your orders открывает стр All orders с информацией о текущих и предидущих заказах.

![order checoutOrder_PastOrderDetails1new](https://user-images.githubusercontent.com/50864552/213482527-e295dcee-88af-4680-988d-ddc491a3ebba.png)
___________
+ На Мастер-странице админ области в основном меню расположены иконки Гамбургер и Выход. Боковое меню влючает ссылки на стр Categories, Products, Orders.
+ На стр Admin/Products в подменю доступна кнопка Add new, фильтрация продуктов по категориям All Products и форма поиска c плейсхолдером Search. Информация о продуктах выводится в табличном виде.
+ Справа стр Product Edit для редактирования продукта.
+ 
![admin products_productEdit1new](https://user-images.githubusercontent.com/50864552/213479252-790de912-c4c3-4f3c-88bb-7b1aa6df6c2d.png)
__________________
+ Стр Admin/Categories отображает категории продуктов.
+ Стр Admin/Orders отображает все заказы.

![admin categories_orders3new](https://user-images.githubusercontent.com/50864552/213482951-865346f0-e431-437a-93d8-cb9ce7d53775.png)
