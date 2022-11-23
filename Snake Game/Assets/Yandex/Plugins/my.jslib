mergeInto(LibraryManager.library, {

  SaveExtern: function (data) {
    var dataString = UTF8ToString(data);
    var myObj = JSON.parse(dataString);
    player.setData(myObj);
  },

  LoadExtern: function () {
    player.getData().then(_data=> {
      const myJSON = JSON.stringify(_data);
      myGameInstance.SendMessage('DataPersist', 'LoadYandexData',myJSON);
    })
  },

  GetLang: function () {
    var returnStr = ysdk.environment.i18n.lang;
    var bufferSize = lengthBytesUTF8(returnStr) + 1;
    var buffer = _malloc(bufferSize);
    stringToUTF8(returnStr, buffer, bufferSize);
    return buffer;
  },

  BuySkin: function (skinName) {
  	var name = UTF8ToString(skinName);
    payments.purchase({ id: name }).then(purchase => {
        // Покупка успешно совершена!
        myGameInstance.SendMessage('DataPersist', 'UnlockSkin', name);
    }).catch(err => {
        // Покупка не удалась: в консоли разработчика не добавлен товар с таким id,
        // пользователь не авторизовался, передумал и закрыл окно оплаты,
        // истекло отведенное на покупку время, не хватило денег и т. д.
    })
  },

  BuyAllSkins: function () {
    payments.purchase({ id: 'unlockallskins' }).then(purchase => {
        // Покупка успешно совершена!
        myGameInstance.SendMessage('DataPersist', 'UnlockAllSkins');
    }).catch(err => {
        // Покупка не удалась: в консоли разработчика не добавлен товар с таким id,
        // пользователь не авторизовался, передумал и закрыл окно оплаты,
        // истекло отведенное на покупку время, не хватило денег и т. д.
    })
  },

  GetMode: function () {
    var returnStr = player.getMode();
    var bufferSize = lengthBytesUTF8(returnStr) + 1;
    var buffer = _malloc(bufferSize);
    stringToUTF8(returnStr, buffer, bufferSize);
    return buffer;
  },

  ShowAdv: function(){
  	ysdk.adv.showFullscreenAdv({
    callbacks: {
        onClose: function(wasShown) {
        	console.log("---------close-----------");
          // some action after close
        },
        onError: function(error) {
          // some action on error
        }
    }
})
  },

});