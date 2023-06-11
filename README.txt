Для проверки работы программы:

1. Репозитории с проектом находится по адресу: https://github.com/28-ralo-7/PracticeControl/tree/ForReview

2. Для запуска всего решения открыть файл по пути: WPF-Client/PracticeControl.sln 

3. Бэкап базы данных PostgreSQL называется productionPracticeControl_backup_forReview_20230608.sql

4. Для проверки работы мобильного приложения Xamarin.Forms нужно: 
 	1) Установить расширение conveyor by keyoti для Visual Studio
	2) Запустить сервер и в панели conveyor by keyoti нажать кнопку "Access Over Internet". 
	В открывшемя окне выбрать бесплатную версию и нажать кнопку "Continue".
	3) В открывшемся окне авторизации ввести:
		Почта: vorden.rolln33@gmail.com
		Пароль: ozobus03
	4) В панели conveyor by keyoti в столбце Internet URL должен появиться url ссылка для обращения
	к webAPI. Пример ссылки: https://longgreyapple95.conveyor.cloud/swagger
	5) Скопировать ссылку без /swagger. Пример: https://longgreyapple95.conveyor.cloud
	6) В проекте PracticeControl.XamarinClient, папка API, файл APIService.cs, строка 15 изменить значение
	переменной urlPath на скопированную ссылку. Пример:
	public static string urlPath = "https://longgreyapple95.conveyor.cloud";
	7) После этого можно запустить проекты WebAPI и XamarinClient
	
5. Данные для входа:
	Для десктопного приложения WPF:		|	Для мобильного приложения Xamarin.Forms.
	Логин: Test				|	Логин: 1
	Пароль: Test				|	Пароль: 1

