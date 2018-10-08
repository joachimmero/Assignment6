using System;
using System.ComponentModel.DataAnnotations;

namespace ApiTeht{
    public class DateTimeValidationAttribute : ValidationAttribute{
    
        public override bool IsValid(object value){
            if(value != null){
                try{
                    if((DateTime)value > DateTime.Now){
                        throw new ArgumentException("Creation Time invalid!");
                    }
                    else{
                        return true;
                    }
                }catch(ArgumentException e){
                    Console.WriteLine(e.Message);
                }
            }
            return false;
        }
    }
    public class AllowedTypeAttribute : ValidationAttribute{
    
        public override bool IsValid(object value){
            if(value != null){
                try{
                    if((string)value != "Megis Burggi"){
                        throw new ArgumentException("Type invalid!");
                    }
                    else{
                        return true;
                    }
                }catch(ArgumentException e){
                    Console.WriteLine(e.Message);
                }
            }
            return false;
        }
    }
}