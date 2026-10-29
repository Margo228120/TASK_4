# Отчёт. День 4

По каждому заданию: что было не так и как ты это заметил. Одной-двумя фразами.

**Задание 1 (пустой столбец):**

**в строке 43 заголовок остаток показывался в столбце price.**

**В строке 41 была допущена орфографическая ошибка.**

**Исправила Price на Stock**

&#x20;**Исправила Prise на price**

**Задание 2 (фильтр по категории):**

**Идет сравнение Id товара с Id категории из-за чего выходит бессмыслица**

**Исправила в строке 83 p.Id p.CategoryId**

**Задание 3 (поиск):**

**учитывался регистр при поиске**

**Ипспрвила: добавила в скобках StringComparison.OrdinalIgnoreCase что позволило использовать при поиске слова не только поиск с маленькой буквы но и с любой заглавной или капсом**

**Задание 6 (чужой состав заказа): получаем  товары для всех заказов клиента**

**Исправила: заменила ClientID на OrderId в строке 131**

**Задание 8 (новый товар):**

**StockBox не читалось, добавила await LoadProducts(); для обновления списка**

**Что было самым сложным:**

**задание 9 и его склейка с нуля**



**///**





**задание 7** 

**var totalSum = items.Sum(item => item.Price \* item.Quantity);**

**TotalText.Text = $"Сумма заказа: {totalSum} руб.";**

**задание 9**

&#x20;   **private async void SetStatus\_Click(object? sender, RoutedEventArgs e)**

&#x20;   **{**



&#x20;       **var order = OrdersGrid.SelectedItem as Order;**

&#x20;       **if (order == null)**

&#x20;       **{**

&#x20;           **ShowMessage("Выберите заказ");**

&#x20;           **return;**

&#x20;       **}**

&#x20;       **var status = StatusBox.SelectedItem as string;**

&#x20;       **if (status == null)**

&#x20;       **{**

&#x20;           **ShowMessage("Выберите статус");**

&#x20;           **return;**

&#x20;       **}**

&#x20;       **var error = await Api.SetStatus(order.Id, status);**

&#x20;       **if (error != null)**

&#x20;       **{**

&#x20;           **ShowMessage("error");**

&#x20;           **return;**

&#x20;       **}**

&#x20;       **ShowMessage($"Статус заказа {order.Id} обновлен на {status}");**

&#x20;       **await LoadOrders();**



&#x20;   **}**

**}**



