<?php

namespace Database\Seeders;

use Illuminate\Database\Console\Seeds\WithoutModelEvents;
use Illuminate\Database\Seeder;
use App\Models\Desk;


class DeskSeeder extends Seeder
{
    /**
     * Run the database seeds.
     */
    public function run(): void
    {
        Desk::create( [
            'id'=>1,
            'number_of_seats'=>4
            ] );
            
            
                        
        Desk::create( [
        'id'=>2,
        'number_of_seats'=>6
        ] );
                    
        Desk::create( [
        'id'=>3,
        'number_of_seats'=>8
        ] );
                    
        Desk::create( [
        'id'=>4,
        'number_of_seats'=>12
        ] );
                    
        Desk::create( [
        'id'=>5,
        'number_of_seats'=>8
        ] );
                    
        Desk::create( [
        'id'=>6,
        'number_of_seats'=>18
        ] );
                    
        Desk::create( [
        'id'=>7,
        'number_of_seats'=>9
        ] );
                    
        Desk::create( [
        'id'=>8,
        'number_of_seats'=>16
        ] );
                    
        Desk::create( [
        'id'=>9,
        'number_of_seats'=>18
        ] );
                    
        Desk::create( [
        'id'=>10,
        'number_of_seats'=>4
        ] );
                    
        Desk::create( [
        'id'=>11,
        'number_of_seats'=>14
        ] );
                    
        Desk::create( [
        'id'=>12,
        'number_of_seats'=>16
        ] );
                    
        Desk::create( [
        'id'=>13,
        'number_of_seats'=>8
        ] );
                    
        Desk::create( [
        'id'=>14,
        'number_of_seats'=>4
        ] );
                    
        Desk::create( [
        'id'=>15,
        'number_of_seats'=>4
        ] );
                    
        Desk::create( [
        'id'=>16,
        'number_of_seats'=>12
        ] );
                    
        Desk::create( [
        'id'=>17,
        'number_of_seats'=>5
        ] );
                    
        Desk::create( [
        'id'=>18,
        'number_of_seats'=>20
        ] );
                    
        Desk::create( [
        'id'=>19,
        'number_of_seats'=>8
        ] );
                    
        Desk::create( [
        'id'=>20,
        'number_of_seats'=>10
        ] );
    }
}
