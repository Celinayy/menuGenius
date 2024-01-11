<?php

namespace Database\Factories;

use Illuminate\Database\Eloquent\Factories\Factory;
use App\Models\Purchase;
use App\Models\Product;

/**
 * @extends \Illuminate\Database\Eloquent\Factories\Factory<\App\Models\Model>
 */
class ProductPurchaseFactory extends Factory
{
    /**
     * Define the model's default state.
     *
     * @return array<string, mixed>
     */
    public function definition(): array
    {
        $this->faker->locale('hu_HU');
        $productId = DB::table('products') -> inRandomOrder() -> value('id');

        return [
            'quantity' => $this -> faker -> numberBetween(1, 3),
            'product_id' =>  $productId,
            'purchase_id' =>  Purchase::factory(),
        ];
    }
}
