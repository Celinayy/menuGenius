<?php

namespace Database\Seeders;

use Illuminate\Database\Console\Seeds\WithoutModelEvents;
use Illuminate\Database\Seeder;
use App\Models\Allergen;


class AllergenSeeder extends Seeder
{
    /**
     * Run the database seeds.
     */
    public function run(): void
    {
        Allergen::create( [
            'id'=>1,
            'code'=>1,
            'name'=>'glutén'
            ] );
                        
        Allergen::create( [
        'id'=>2,
        'code'=>1.1,
        'name'=>'búza'
        ] );
                    
        Allergen::create( [
        'id'=>3,
        'code'=>1.2,
        'name'=>'rozs'
        ] );
                    
        Allergen::create( [
        'id'=>4,
        'code'=>1.3,
        'name'=>'árpa'
        ] );
                    
        Allergen::create( [
        'id'=>5,
        'code'=>1.4,
        'name'=>'zab'
        ] );
                    
        Allergen::create( [
        'id'=>6,
        'code'=>1.5,
        'name'=>'tönköly'
        ] );
                    
        Allergen::create( [
        'id'=>7,
        'code'=>1.6,
        'name'=>'kamut búza'
        ] );
                    
        Allergen::create( [
        'id'=>8,
        'code'=>2,
        'name'=>'rákfélék'
        ] );
                    
        Allergen::create( [
        'id'=>9,
        'code'=>3,
        'name'=>'tojás'
        ] );
                    
        Allergen::create( [
        'id'=>10,
        'code'=>4,
        'name'=>'hal'
        ] );
                    
        Allergen::create( [
        'id'=>11,
        'code'=>5,
        'name'=>'mogyoró'
        ] );
                    
        Allergen::create( [
        'id'=>12,
        'code'=>6,
        'name'=>'szójabab'
        ] );
                    
        Allergen::create( [
        'id'=>13,
        'code'=>7,
        'name'=>'tej'
        ] );
                    
        Allergen::create( [
        'id'=>14,
        'code'=>8,
        'name'=>'diófélék'
        ] );
                    
        Allergen::create( [
        'id'=>15,
        'code'=>8.1,
        'name'=>'mandula'
        ] );
                    
        Allergen::create( [
        'id'=>16,
        'code'=>8.2,
        'name'=>'törökmogyoró'
        ] );
                    
        Allergen::create( [
        'id'=>17,
        'code'=>8.3,
        'name'=>'dió'
        ] );
                    
        Allergen::create( [
        'id'=>18,
        'code'=>8.4,
        'name'=>'kesudió'
        ] );
                    
        Allergen::create( [
        'id'=>19,
        'code'=>8.5,
        'name'=>'pekándió'
        ] );
                    
        Allergen::create( [
        'id'=>20,
        'code'=>8.6,
        'name'=>'paradió'
        ] );
                    
        Allergen::create( [
        'id'=>21,
        'code'=>8.7,
        'name'=>'pisztácia'
        ] );
                    
        Allergen::create( [
        'id'=>22,
        'code'=>8.8,
        'name'=>'makadámdió'
        ] );
                    
        Allergen::create( [
        'id'=>23,
        'code'=>9,
        'name'=>'zeller'
        ] );
                    
        Allergen::create( [
        'id'=>24,
        'code'=>10,
        'name'=>'mustár'
        ] );
                    
        Allergen::create( [
        'id'=>25,
        'code'=>11,
        'name'=>'szezám'
        ] );
                    
        Allergen::create( [
        'id'=>26,
        'code'=>12,
        'name'=>'kén-dioxid'
        ] );
                    
        Allergen::create( [
        'id'=>27,
        'code'=>13,
        'name'=>'csillagfürt'
        ] );
                    
        Allergen::create( [
        'id'=>28,
        'code'=>14,
        'name'=>'puhatestűek'
        ] );
                    
        Allergen::create( [
        'id'=>29,
        'code'=>15,
        'name'=>'mesterséges édesítőszerek'
        ] );
                    
        Allergen::create( [
        'id'=>30,
        'code'=>16,
        'name'=>'édesgyökér'
        ] );
    }
}
