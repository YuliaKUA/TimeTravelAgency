# TimeTravelAgency - проект для вымышленного туристического агенства
## Используемые технологии: 
### Бэкенд:
- ASP.NET Core
- DDD
- EF Core
- MS SQL Server
- Аутентификация и авторизация (Cookie)
- Логгирование NLog
- Юнит тестирование xUnit + Moq
### Фронтенд:
- Html/Css + Bootstrap
- jQuery/Ajax
### Данные:
- Bogus: генерация данных для БД
- Midjourney AI: сгенерированные изображения
- ChatGpt: сгенерированный текст

### База данных
![](https://github.com/YuliaKUA/TimeTravelAgency/blob/develop/Attachment%20files/BD.png)

## Подробнее о приложении 
### Обзор приложения
На главной странице расположены окно с эффектом параллакса, ознакомительная информация о "компании", а также ссылки на другие разделы. 

Благодаря встроенной системе сеток Bootstrap - дизайн адаптивен под разные устройства.

Все изображения + информация о турах хранятся в базе данных в виде массива байтов (и строк соответственно).

![](https://github.com/YuliaKUA/TimeTravelAgency/blob/develop/Attachment%20files/overview.gif)
### Регистрация пользователя
Просматривать информацию может любой пользователь, однако, чтобы сделать и оплатить заказ - необходимо зарегистрироваться.

В базе данных хранится хэш пароля(алгоритм sha256).

![](https://github.com/YuliaKUA/TimeTravelAgency/blob/develop/Attachment%20files/registration.gif)

![](https://github.com/YuliaKUA/TimeTravelAgency/blob/develop/Attachment%20files/user%20authentication.gif)

### Аутентификация и авторизация
Важное место в приложении занимает аутентификация и авторизация. 

ASP.NET Core имеет встроенную поддержку аутентификации на основе куки. При получении запроса от клиента, в котором содержатся аутентификационные куки, происходит их валидация, десериализация и инициализация свойства User объекта HttpContext. Для установки куки применяется асинхронный метод контекста HttpContext.SignInAsync(). Объект ClaimsPrincipal (представляющий пользователя) содержит список claims из Id, Login, Role пользователя.

Ключевым инструментом для авторизации является атрибут Authorize, предотвращающий неаутентифицированный доступ к определенным методам (например "корзина"). Если анонимный пользователь попытается обратиться к таким методам, то его перенаправит по пути Home/Index.

В приложении присутствуют 3 типа пользователей. У каждого из них разный уровень доступа к информации и функций приложения:

Пользователь:
- Основная информация сайта (Главная страница, Путеводитель, Туры, О нас)
- Возможность изменять данные своего профиля (фио, почта, телефон и т.д.)
- Возможность добавлять туры в корзину
- Возможность создавать и оплачивать заказ
  
Модератор:
- То же, что пользователь
- Возможность добавлять/удалять/изменять туры

Администратор:
- То же, что модератор
- Доступ ко всем учетным записям
- Возможность удалять/изменять учетные записи

В интерфейсе это реализовано через дополнительные вкладки и кнопки для соответствующих ролей. 
![](https://github.com/YuliaKUA/TimeTravelAgency/blob/develop/Attachment%20files/authorization.gif)

### Уровень доступа "Администратор"
![](https://github.com/YuliaKUA/TimeTravelAgency/blob/develop/Attachment%20files/administrator.gif)

### Валидация
Все формы проверяют значения, указанные пользователем, и отображают найденные ошибки.
![](https://github.com/YuliaKUA/TimeTravelAgency/blob/develop/Attachment%20files/validation.gif)

### Пример использования приложения пользователем
![](https://github.com/YuliaKUA/TimeTravelAgency/blob/develop/Attachment%20files/user%20creates%20order.gif)