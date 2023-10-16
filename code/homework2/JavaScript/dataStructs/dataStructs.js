"use strict";

// List (or Array or LinkedList):
let cars_list = [
  'Saab',
  'Volvo',
  'BMW'
];

// loop by indexes
for(let i = 0; i < cars_list.length; i++){
  // This is to get the i-th element
  let element = cars_list[i];
}

// Remove first element
cars_list.pop(0)
// Add an element
cars_list.push('Toyota');
// set an element
cars_list[0] = 'Fiat';
// check the existance
cars_list.includes('Volvo'); //true

// Dictionary
let car_dict = {Brand: 'Fiat', Model: 'Punto', Color: 'White'}

// loop on dictionary
for(const [key, value] of Object.entries(car_dict)){
  console.log(key);
  console.log(value);
}
// get a value given the key
let element = car_dict['Brand'];
// set an element
car_dict['Model'] = 'Panda';
// Check key existance
console.log('key' in car_dict);
// Check value existance, to do so we must first create an array with all the values
let car_dict_values = []
for(const [key, value] of Object.entries(car_dict)){ car_dict_values.push(value); }
console.log(car_dict_values.includes('value'));

// Sorted list
class SortedList {
  constructor(){
    let array = Array.from(arguments);
    array.sort();
  }

  push(elem) { array.push(elem); array.sort(); }
  remove(n) { array.pop(n); }
  includes(elem) { array.includes(elem); }
}


// Set ( or hashset )
let cars_set = new Set();
cars_set.add('Volvo');
cars_set.has('Volvo');
cars_set.remove('Volvo');

// An ordinated set doesn't exists since a Set is an unordinated group of data

// Queue
class Queue {
  constructor(){
    let array = Array.from(arguments);
  }

  // A queue follows a FIFO policy (First In First Out)
  push(elem) { array.push(elem); }
  remove() { array.pop(0); }
  includes(elem) { array.includes(elem); }
}

// A Stack on the other hand, follows a LIFO policy (Last In, Fist Out)
class Stack {
  constructor(){
    let array = Array.from(arguments);
  }

  // A queue follows a FIFO policy (First In First Out)
  push(elem) { array.push(elem); }
  remove() { array.pop(array.length - 1); }
  includes(elem) { array.includes(elem); }
}