<?php

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

Route::get(uri: '/payform', action: 'App\Http\Controllers\StripeController@index')->name(name:'index');
Route::post(uri: '/checkout', action: 'App\Http\Controllers\StripeController@checkout')->name(name:'checkout');
Route::post(uri: '/success', action: 'App\Http\Controllers\StripeController@handleSuccess')->name(name:'handle.success');
//Route::match(['get', 'post'], '/success', 'App\Http\Controllers\StripeController@handleSuccess')->name('success');
Route::view('/success', 'success')->name('success');
