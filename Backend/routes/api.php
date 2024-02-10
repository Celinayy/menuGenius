<?php

use Illuminate\Http\Request;
use Illuminate\Support\Facades\Route;
use App\Http\Controllers\AllergenController;
use App\Http\Controllers\CategoryController;
use App\Http\Controllers\DeskController;
use App\Http\Controllers\EventLogController;
use App\Http\Controllers\ImageController;
use App\Http\Controllers\IngredientController;
use App\Http\Controllers\ProductController;
use App\Http\Controllers\PurchaseController;
use App\Http\Controllers\ReservationController;
use App\Http\Controllers\UserController;

/*
|--------------------------------------------------------------------------
| API Routes
|--------------------------------------------------------------------------
|
| Here is where you can register API routes for your application. These
| routes are loaded by the RouteServiceProvider and all of them will
| be assigned to the "api" middleware group. Make something great!
|
*/

use App\Http\Controllers\AuthController;
use App\Http\Controllers\SocialiteController;

Route::controller(AuthController::class)->group(function () {
    Route::middleware('eventlogger')->post('/register', 'register');
    Route::middleware('eventlogger')->post('/login', 'login');
    Route::middleware(['auth:sanctum', 'eventlogger'])->post('/logout', 'logout');
});

Route::controller(ProductController::class)->group(function () {
    Route::middleware('auth:sanctum')->get('product/userFavorites', 'userFavorites');
    Route::get('product', 'index');
    Route::get('product/{id}', 'show');
    Route::middleware('auth:sanctum')->post('product/{id}/addToFavorites', 'addToFavorites');
    Route::middleware('auth:sanctum')->post('product/{id}/removeFromFavorites', 'removeFromFavorites');
});

Route::controller(PurchaseController::class)->group(function () {
    Route::middleware('auth:sanctum')->get('purchase', 'index');
    Route::middleware('auth:sanctum')->get('purchase/{id}', 'show');
    Route::middleware(['auth:sanctum', 'eventlogger'])->put('purchase/{id}', 'update');
    Route::middleware('eventlogger')->post('purchase', 'store');
});

Route::controller(ReservationController::class)->group(function () {
    Route::get('reservation/checkAvailableDesk', 'checkAvailableDesk');
    Route::middleware('auth:sanctum')->get('reservation', 'index');
    Route::middleware('auth:sanctum')->get('reservation/{id}', 'show');
    Route::middleware(['auth:sanctum', 'eventlogger'])->put('reservation/{id}', 'update');
    Route::middleware(['auth:sanctum', 'eventlogger'])->delete('reservation/{id}', 'destroy');
    Route::middleware('eventlogger')->post('reservation', 'store');
});

Route::controller(UserController::class)->group(function (){
    Route::middleware('auth:sanctum')->get('user', 'index');
    Route::middleware('auth:sanctum')->get('user/{id}', 'show');
    Route::middleware(['auth:sanctum', 'eventlogger'])->put('user', 'update');
    Route::middleware(['auth:sanctum', 'eventlogger'])->delete('user/{id}', 'destroy');
    Route::middleware(['auth.universal', 'eventlogger'])->post('user', 'store');
});

Route::get("allergen", [AllergenController::class, 'index']);
Route::get("category", [CategoryController::class, 'index']);
Route::get("ingredient", [IngredientController::class, 'index']);
Route::get("desk", [DeskController::class, 'index']);

Route::get('/charge', function () {
    return view('charge');
});

