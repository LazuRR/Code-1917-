## Архитектура
#### Тип приложения
  Приложение Code-1917 постороено по принципу микросервисной архитектуры и сотоит из следующих микросервисов: 
- Game - отвечают за обработку всей логики приложения
- Animation - микросервис, отвечающий за анимацию событийных карточек 
- Graphics - микросервис, отвечающий за всю графику приложения, а также корректное отображение всех сущностей
- Unity - взаимодействует с пользователем и связывающий между собой три предыдущих микросервиса. 
Микросервисную архитектуру отличает большая приспособленность к командной разработке.
Особенностью данной архитектуры также является ее гибкость к изменениям, что позволяет вносить какие-либо изменения в модуль, при этом работа других модулей не будет меняться. Минусом данной архитектуры является ее сложность. 
 
#### Стратегия развертывания
  Собранный apk-файл должен быть установлен на телефоне, а затем запущен.
  

#### Используемые технологии
  Приложение написано на кроссплатформенным игровом движке Unity. Unity — хороший выбор для создания средних по сложности проектов как для ПК, так и для мобильных устройств. Один из козырей Юнити — это список поддерживаемых платформ, где может запускаться приложения. Unity работает почти везде — в том числе и на Андроид. Также используется анимация DoTween и шрифтовой ресурс TextMeshPro для отображения текста.

#### Показатели качества
 - Возможность собрать приложение на IOS
 - Малый вес приложение
 - Отсутствие лагов
 - Полнота реализации функционала, определенного в User Stories
 - Возможность добавления новых историй без поломки старых
 

## Анализ архитектуры
Набор пакетов, общих для микросервисов Game, Animation и Graphics:
  - Card - класс, содержащий текст, вариант ответа и изображение события. 
  - дополнить!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
  
Пакет, определенный только в микросервисе Animation:
  - CardAnimation - класс, отвечающий за анимацию появления карточек.
  
Классы клиентской части приложения:
  - StoryManager - класс, связанный с Untig для связывания историй визуально для пользователя.
  - GameManager - класс, использующийся для запуска игры.
  - UiManager - класс, связанный с Card для связывания игровой логики.
  - GameManager - класс, отвечающий за логику появления других карточек.

## Сравнение архитектур As Is и To Be
Архитектуры As Is и To Be получились почти одинаковыми, исключениями являются добавление пакета cardAnimation в Animation. Столь незначительные различия можно объяснить довольно простой системой сущностей и взаимосвязей между ними. Пути улучшения архитектуры:

Убрать связывание историй и GameMangaer, чтобы истории связывались только с StoryManager.