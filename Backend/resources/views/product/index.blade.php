<form action="{{ route('checkout') }}" method="POST">
    @csrf
    <label for="desk_id">Desk ID:</label>
    <input type="text" name="desk_id" id="desk_id" value="">
    <br>
    <label for="product1">Product 1 ID:</label>
    <input type="text" name="products[]" id="product1" value="">
    <br>
    <label for="product2">Product 2 ID:</label>
    <input type="text" name="products[]" id="product2" value="">
    <br>
    <label for="product2">Product 3 ID:</label>
    <input type="text" name="products[]" id="product3" value="">
    <br>
    <label for="product2">Product 4 ID:</label>
    <input type="text" name="products[]" id="product4" value="">
    <br>
    <label for="product2">Product 5 ID:</label>
    <input type="text" name="products[]" id="product5" value="">
    <br>
    <button type="submit">Checkout</button>
</form>
