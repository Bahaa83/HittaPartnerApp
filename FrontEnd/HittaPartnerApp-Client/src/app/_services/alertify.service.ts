import { Message } from '@angular/compiler/src/i18n/i18n_ast';
import { Injectable } from '@angular/core';
import  * as alertify from 'alertifyjs';


@Injectable({
  providedIn: 'root'
})
export class AlertifyService {

constructor() { }

      confirm(message:string,okCallback:()=>any):any
      {
        alertify.confirm(message,function(e:Event){
          if(e){okCallback()}else{}
        }
        
        );
      }

      promisifyConfirm(title: string, message: string, options = {}): Promise<ConfirmResult> {

        return new Promise<ConfirmResult>((resolve) => {
          alertify.confirm(
            title,
            message,
            () => resolve(ConfirmResult.Ok),
            () => resolve(ConfirmResult.Cancel)).set(Object.assign({}, {
              closableByDimmer: false,
              defaultFocus: 'cancel',
              frameless: false,
              closable: false
            }, options));
        });
      }
      
      
    
      
        success(message:string)
        {
          alertify.success(message);
        }
        warning(message:string)
        {
          alertify.warning(message);
        }
        error(message:string)
        {
          alertify.eroor(message);
        }
        message(message:string)
        {
          alertify.message(message);
        }


              //defaults
      
defaults=alertify.defaults = {
  // dialogs defaults
  autoReset: true,
    basic: false,
    closable: true,
    closableByDimmer: true,
    frameless: false,
    maintainFocus: true, // <== global default not per instance, applies to all dialogs
    maximizable: true,
    modal: true,
    movable: true,
    moveBounded: false,
    overflow: true,
    padding: true,
    pinnable: true,
    pinned: true,
    preventBodyShift: false, // <== global default not per instance, applies to all dialogs
    resizable: true,
    startMaximized: false,
    transition: 'pulse',
  // notifier defaults
  notifier:{
  // auto-dismiss wait time (in seconds)  
      delay:2,
  // default position
      position:'bottom-right',
  // adds a close button to notifier messages
      closeButton: false,
  // provides the ability to rename notifier classes
      classes : {
          base: 'alertify-notifier',
          prefix:'ajs-',
          message: 'ajs-message',
          top: 'ajs-top',
          right: 'ajs-right',
          bottom: 'ajs-bottom',
          left: 'ajs-left',
          center: 'ajs-center',
          visible: 'ajs-visible',
          hidden: 'ajs-hidden',
          close: 'ajs-close'
      }
  },

  // language resources 
  glossary:{
      // dialogs default title
      title:'Hitta Partner',
      // ok button text
      ok: 'OK',
      // cancel button text
      cancel: 'Avbryt'            
  },

  // theme settings
  theme:{
      // class name attached to prompt dialog input textbox.
      input:'ajs-input',
      // class name attached to ok button
      ok:'ajs-ok',
      // class name attached to cancel button 
      cancel:'ajs-cancel'
  },
  hooks:{
    // invoked before initializing any dialog
    preinit:function(){},
    // invoked after initializing any dialog
    postinit:function(){},
},
}

      }
      export enum ConfirmResult{
        Ok=1,
        Cancel
      }

