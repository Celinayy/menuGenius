<?php

use App\Http\Controllers\StripeController;
use Illuminate\Support\Facades\Route;

/*
|--------------------------------------------------------------------------
| Web Routes
|--------------------------------------------------------------------------
|
| Here is where you can register web routes for your application. These
| routes are loaded by the RouteServiceProvider and all of them will
| be assigned to the "web" middleware group. Make something great!
|
*/

/* Route::get('/', function () {
    return view('welcome');
});
 */

Route::get('/', [StripeController::class, 'index'])->name(name:'index');
Route::post('/checkout', [StripeController::class, 'checkout'])->name(name:'checkout');
Route::get('/success', [StripeController::class, 'success'])->name(name:'checkout.success');
Route::get('/cancel', [StripeController::class, 'cancel'])->name(name:'checkout.cancel');
Route::post('/webhook', [StripeController::class, 'webhook'])->name(name:'checkout.webhook');

//Route::match(['get', 'post'], '/success', 'App\Http\Controllers\StripeController@handleSuccess')->name('success');
//Route::view('/success', 'success')->name('success');
