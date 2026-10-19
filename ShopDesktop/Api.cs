using System.Net;
using System.Net.Http.Json;
using ShopDesktop.Models;

namespace ShopDesktop;

// Всё общение с API — здесь. Этот файл уже готов, менять его не нужно.
//
// Как пользоваться (не забывай слово await!):
//     var products = await Api.GetProducts();
//     var error = await Api.AddProduct(product);   // null — всё хорошо, иначе — текст ошибки
public static class Api
{
    // Адрес, на котором работает ShopAPI (README, шаг 2)
    private static readonly HttpClient http = new() { BaseAddress = new Uri("http://localhost:5000/") };

    // GET /categories — все категории
    public static async Task<List<Category>> GetCategories()
    {
        return await http.GetFromJsonAsync<List<Category>>("categories") ?? new();
    }

    // GET /products — все товары
    public static async Task<List<Product>> GetProducts()
    {
        return await http.GetFromJsonAsync<List<Product>>("products") ?? new();
    }

    // GET /orders — все заказы
    public static async Task<List<Order>> GetOrders()
    {
        return await http.GetFromJsonAsync<List<Order>>("orders") ?? new();
    }

    // GET /orders/{orderId}/items — строки одного заказа
    public static async Task<List<OrderItem>> GetOrderItems(int orderId)
    {
        var response = await http.GetAsync($"orders/{orderId}/items");
        if (response.StatusCode == HttpStatusCode.NotFound)
            return new();
        return await response.Content.ReadFromJsonAsync<List<OrderItem>>() ?? new();
    }

    // POST /products — добавить товар. Возвращает null, если всё хорошо, или текст ошибки
    public static async Task<string?> AddProduct(Product product)
    {
        var response = await http.PostAsJsonAsync("products", product);
        return await GetError(response);
    }

    // PUT /orders/{orderId}/status?status=... — сменить статус заказа. Возвращает null или текст ошибки
    public static async Task<string?> SetStatus(int orderId, string status)
    {
        var response = await http.PutAsync($"orders/{orderId}/status?status={Uri.EscapeDataString(status)}", null);
        return await GetError(response);
    }

    // Превращает ответ API в текст ошибки. Ответ успешный — null
    private static async Task<string?> GetError(HttpResponseMessage response)
    {
        if (response.IsSuccessStatusCode)
            return null;
        if (response.StatusCode == HttpStatusCode.NotFound)
            return "Не найдено";

        var text = await response.Content.ReadAsStringAsync();
        if (string.IsNullOrWhiteSpace(text) || text.StartsWith("{"))
            return $"Ошибка {(int)response.StatusCode}";
        return text;
    }
}
