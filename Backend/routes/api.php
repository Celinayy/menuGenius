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
    Route::post('/register', 'register');
    Route::post('/login', 'login');
    Route::middleware('auth:sanctum')->post('/logout', 'logout');
});
Route::controller(ProductController::class)->group(function () {
    Route::get('product', 'index');
    Route::get('product/{id}', 'show');
});

Route::controller(PurchaseController::class)->group(function () {
    Route::middleware('auth:sanctum')->get('purchase', 'index');
    Route::middleware('auth:sanctum')->get('purchase/{id}', 'show');
    Route::middleware('auth:sanctum')->put('purchase/{id}', 'update');
    Route::post('purchase', 'store');
});

Route::controller(ReservationController::class)->group(function () {
    Route::middleware('auth:sanctum')->get('reservation', 'index');
    Route::middleware('auth:sanctum')->get('reservation/{id}', 'show');
    Route::middleware('auth:sanctum')->put('reservation/{id}', 'update');
    Route::post('reservation', 'store');
});

Route::middleware('auth:sanctum')->resource("user", UserController::class)->except(["edit", "create"]);
Route::get("allergen", [AllergenController::class, 'index']);
Route::get("category", [CategoryController::class, 'index']);
Route::get("ingredient", [IngredientController::class, 'index']);

//Route::post('/login/callback', [SocialiteController::class, 'handleProviderCallback']);
//Route::middleware(['auth:sanctum', 'permission:admin'])->resource("eventlog", EventLogController::class)->except(["edit", "create"]);
//Route::middleware('auth:sanctum')->get('/user', function (Request $request) {
//    return $request->user();
//});

// Route::middleware(['auth:sanctum', 'permission:guest'])->group(function () {
//     Route::put('reservation/{id}', [ReservationController::class, 'update']);
//     Route::delete('reservation/{id}', [ReservationController::class, 'destroy']);
// });

//Route::middleware(['permission:guest'])->post('reservation', ReservationController::class);
//Route::middleware('auth:sanctum')->get('reservation/{id}', [ReservationController::class, 'show']);
//Route::middleware('auth:sanctum')->resource("desk", DeskController::class)->except(["edit", "create"]);
//Route::resource("image", ImageController::class)->except(["edit", "create"]);
