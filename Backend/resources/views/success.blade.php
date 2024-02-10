<!DOCTYPE html>
<html lang="en">
<head>
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <title>Payment Success</title>
    <!-- Add your CSS stylesheets or CDN links here -->
</head>
<body>
    <h1>Payment Successful</h1>
    <p>Thank you for your purchase!</p>
    <form action="{{ route('handle.success') }}" method="POST">
        @csrf
        <input type="hidden" name="stripe_id" value="{{ $session_id }}">
        <label for="total_pay">Total Pay:</label>
        <input type="text" name="total_pay" value="{{ $amount_total /100 }}" disabled>
        <input type="hidden" name="date_time" value="{{ now() }}">
        <label for="desk_id">Desk ID:</label>
        <input type="text" name="desk_id" value="">
        <!-- További mezők hozzáadása szükség esetén -->
        <button type="submit">Vásárlás rögzítése</button>
    </form>
    </body>
</html>

