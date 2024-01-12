<?php

namespace Database\Seeders;

use Illuminate\Database\Console\Seeds\WithoutModelEvents;
use Illuminate\Database\Seeder;
use App\Models\Category;


class CategorySeeder extends Seeder
{
    /**
     * Run the database seeds.
     */
    public function run(): void
    {
        Category::create( [
        'id'=>1,
        'name'=>'előétel'
        ] );
                        
        Category::create( [
        'id'=>2,
        'name'=>'leves'
        ] );
                    
        Category::create( [
        'id'=>3,
        'name'=>'sertéshús'
        ] );
                    
        Category::create( [
        'id'=>4,
        'name'=>'szárnyashús'
        ] );
                    
        Category::create( [
        'id'=>5,
        'name'=>'marhahús'
        ] );
                    
        Category::create( [
        'id'=>6,
        'name'=>'hal'
        ] );
                    
        Category::create( [
        'id'=>7,
        'name'=>'vegetáriánus'
        ] );
                    
        Category::create( [
        'id'=>8,
        'name'=>'tészta'
        ] );
                    
        Category::create( [
        'id'=>9,
        'name'=>'saláta'
        ] );
                    
        Category::create( [
        'id'=>10,
        'name'=>'köret'
        ] );
                    
        Category::create( [
        'id'=>11,
        'name'=>'savanyúság'
        ] );
                    
        Category::create( [
        'id'=>12,
        'name'=>'desszert'
        ] );
                    
        Category::create( [
        'id'=>13,
        'name'=>'üdítő'
        ] );
                    
        Category::create( [
        'id'=>14,
        'name'=>'kávé'
        ] );
                    
        Category::create( [
        'id'=>15,
        'name'=>'sör'
        ] );
                    
        Category::create( [
        'id'=>16,
        'name'=>'bor'
        ] );
                    
        Category::create( [
        'id'=>17,
        'name'=>'rövidital'
        ] );
                    
        Category::create( [
        'id'=>18,
        'name'=>'pezsgő'
        ] );
                    
        Category::create( [
        'id'=>19,
        'name'=>'tea'
        ] );
                    
        Category::create( [
        'id'=>20,
        'name'=>'koktél'
        ] );
    }
}
