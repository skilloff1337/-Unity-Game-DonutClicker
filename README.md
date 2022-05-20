# DonutClicker [Unity 2021.2.10f1]

**EN: **

A casual game where you need to conquer the world with your donuts, so that everyone talks about your donuts in every nook and cranny. 
To build an empire, you have to curse a donut, and then build an empire on the accumulated donuts, buying business after business.

**RU:**

Казуальная игра, где требуется завоевать мир своими пончиками, что бы о ваших пончиках говорили в каждом закоулочке. 
Что бы построить империю, придется поклилать пончик, а затем на накопленные пончики - строить империю, покупаю бизнес за бизнесом.

**Dowload build version -**
## Used technologies | Использованные технологии

1. **Zenject** - Dependency Injection. 
2. **MongoDB** - DataBase.
3. **Newtonsoft** - Work with Json.
4. **Cryptography** - Work with Encrypt/Decrypt local files.

## Gaming systems | Игровые системы 

1. **Shop | Магазин** 

**<p>EN:</p>**
Dynamic store system, when buying an item, the player will start receiving donuts per second, depending on the level of the business, the business itself and improvements to it.
To add new items, you need to create a ScriptableObject (RMB -> Create -> ScriptableObject -> CreateNewShopItem), configure it.
And then add it to the array in the hierarchy, on the "Panel_Shop" object.
**<p>RU:</p>**
Динамическая система магазина, при покупке предмета, игрок начнет получать пончики в секунду, в зависимости от уровня бизнеса, самого бизнеса и улучшений к нему.
Для добавления новых предметов, нужно создать ScriptableObject (ПКМ -> Create -> ScriptableObject -> CreateNewShopItem), настроить его.
А затем добавить его в массив в иерархия, на объекте "Panel_Shop".
![image](https://user-images.githubusercontent.com/101990183/169605804-6215ccd4-52a3-4e8c-b042-b0abb34ff476.png)
![image](https://user-images.githubusercontent.com/101990183/169604707-49987768-8f26-4889-8b15-2a3c0a824f26.png)

2. **Upgrades | Улучшения** 

**<p>EN:</p>**
Improvements, a place where the player can improve their gameplay, like donut upgrades (which should be clicked), businesses bought in stores, click power,
clicked crit chance, crit multiplier, as well as income while the player is out of the game.
**<p>RU:</p>**
Улучшения, место где игрок сможет улучшить свой игровой процесс, как улучшения пончика(по которому следует кликать), бизнесы купленные в магазинах, сила клика,
шанс крита при клике, множитель крита, а также доход пока игрок находится вне игры.
![image](https://user-images.githubusercontent.com/101990183/169604891-a967513d-b7c2-4a05-be85-01cad775d717.png)

3. **Quests | Задания** 

**<p>EN:</p>**
Quests - will introduce the player to the basics of the player, also rewarding for completing quests - a bonus. The system is fully dynamic, to create new tasks,
you need to create a ScriptableObject (RMB -> Create -> ScriptableObject -> CreateNewQuests), configure it. And then add it to an array in the hierarchy, on the "Panel_Quests" object.
**<p>RU:</p>**
Задания - введут игрока по основам игрока, также награждая за выполнения квестов - бонусом. Система полностью динамеческая, для создания новых заданий, 
нужно создать ScriptableObject (ПКМ -> Create -> ScriptableObject -> CreateNewQuests), настроить его. А затем добавить его в массив в иерархия, на объекте "Panel_Quests".
![image](https://user-images.githubusercontent.com/101990183/169605900-9b3d42ac-800e-4dc7-b4db-5a1394be3f9b.png)
![image](https://user-images.githubusercontent.com/101990183/169605363-e5af8195-780e-4743-a397-0240ccd4cb16.png)

4. **Settings | Настройки** 

**<p>EN:</p>**
Settings - a place where you can customize the game for yourself. Select the desired language (Auto-replacement of all texts to the desired language occurs in run-time in **0.0007 seconds**).
To add a new language, you will need to edit the `LanguageEnglish.json` file, in the file in the `Translation` field, change the text to the required one.
And add a class with a repository (Example class `LocalizationEnglishRepository`). You can also change the volume of sound / music / nickname.
**<p>RU:</p>**
Настройки - место где можно настроить игру под себя. Выбрать нужный язык (Авто-замена всех текстов на нужный язык происходит в ран-тайме за **0.0007 секунды**).
Для добавления нового языка, потребуется подрублировать файл `LanguageEnglish.json`, в файле в поле `Translation`изменить текст на требуемый. 
И добавить класс, с репозиториемю (Пример класс `LocalizationEnglishRepository`). Также можно изменить громкость звука/музыки/никнейм.
![image](https://user-images.githubusercontent.com/101990183/169607165-f51fc592-3fb9-455c-9bff-e478ee94af01.png)
![image](https://user-images.githubusercontent.com/101990183/169606150-ec05ed84-804b-495d-a4df-aa0e20ba0059.png)

5. **Statistics | Статистика** 

**<p>EN:</p>**
Statistics - a place where the player can see all the statistics on his game in this game.
**<p>RU:</p>**
Статистика - место, где игрок сможет увидеть всю статистику по своей игре в данную игру.
![image](https://user-images.githubusercontent.com/101990183/169607830-295b9941-2cc1-420d-bd31-6a097f14545c.png)

6. **Achievements | Достижения** 

**<p>EN:</p>**
Achievements - when a player achieves some success, this will be reflected in this tab. Where the player can see the achievement progress, reward, achievement level and also
reward, if any. The system is fully dynamic, to create a new achievement, you need to create a ScriptableObject (RMB -> Create -> ScriptableObject -> CreateNewAchievements),
customize it. And then add it to an array in the hierarchy, on the "Panel_Achievements" object.
**<p>RU:</p>**
Достижения - когда игрок добьется каких-то успехов, это отразится в данной вкладке. Где игрок сможет посмотреть прогресс достяжения, награду, уровень достяжения а также
награду, если такова имется. Система полностью динамеческая, для создания нового достяжения, нужно создать ScriptableObject (ПКМ -> Create -> ScriptableObject -> CreateNewAchievements), 
настроить его. А затем добавить его в массив в иерархия, на объекте "Panel_Achievements".
![image](https://user-images.githubusercontent.com/101990183/169608475-4abdc9e1-0b30-412a-9815-267f8a70d2d2.png)
![image](https://user-images.githubusercontent.com/101990183/169608076-d5d9ba3d-0d55-4531-9d84-47687976f1d0.png)

7. **Top Players | Лучшие игроки** 

**<p>EN:</p>**
Achievements - when a player achieves some success, this will be reflected in this tab. Where the player can see the achievement progress, reward, achievement level and also
reward, if any. The system is fully dynamic, to create a new achievement, you need to create a ScriptableObject (RMB -> Create -> ScriptableObject -> CreateNewAchievements),
customize it. And then add it to an array in the hierarchy, on the "Panel_Achievements" object.
**<p>RU:</p>**
В главном меню, справа снизу, при подключении к базе данных, будет панель с разделами лучших игроков игры по категориям. Зеленым цветом, помечен сам игрок с его статистикой.
Обновление списка происходит раз в 300 секунд(5минут).
![image](https://user-images.githubusercontent.com/101990183/169609804-7f529110-730f-4f48-9c9d-354a4d798286.png)

8. **Localization | Локализация** 

**<p>EN:</p>**
As already mentioned in section 4 (settings), the automatic translation system is quite fast and convenient, especially since the system was written completely independently.
For example, at the time of BootStrapper, loading languages into caching will take **0.003 seconds**(2 languages, 334 words), searching for texts **0.0008 seconds**(370 texts on stage,
of these, 88 need only be edited). Well, changing texts also takes **0.0008 seconds**(88 texts).
**<p>RU:</p>**
Как уже говорилось в разделе 4(настройки), автоматическая система перевода достаточно быстрая и удобная, тем более система, была написано полностью самостоятельно. 
К примеру, в момент BootStrapper'a, загрузка языков в кэширование занимет **0.003 секунды**(2 языка, 334 слова), поиск текстов **0.0008 секунды**(370 текстов на сцене,
из них 88 нужно только редактировать). Ну и изменение текстов, занимает также **0.0008 секунды**(88 текстов).
![image](https://user-images.githubusercontent.com/101990183/169612494-57fe2878-d589-47f3-a913-21c2223246f3.png)


**Also | Также**
**<p>EN:</p>**
There are still other cool systems left in the game (such as anti-clicker, number envelopes, bootstrapper, level system, hint system, notification system)
I advise you to check it out on your own. The finished game can be downloaded from the link located at the very top of this text (line 13).
**<p>RU:</p>**
В игре осталось еще других крутых систем(таких как анти-кликер, конвертов чисел, bootstrapper, система уровня, система подсказок, система уведомлений) 
советую ознакомиться самостоятельно. Готовую игру можно скачать по ссылке, которая рассположена в самом верху этого текста.
