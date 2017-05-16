app.controller('EstadoClienteController', function($scope, $http, appService){ 

    $scope.mostrar = true;
   
	var url = '/EstadoCliente';
	$scope.resultados;
	$scope.object;

	$scope.datos = {
	    Id: '',
        NombreEstado: '',
	}
   
	$scope.showId = function (Id) {
	    //$scope.datos.Id = Id;
	    appService.showId(url + "/ShowId/"+Id)
            .then(function (results) {
                if (results.data !== '') {
                    $scope.object = results.data;
                }
            },
                function (err) {
                    console.log(err);
                });
	}

    //Funcion Ajax para traer la lista de Estado Cliente
	$scope.show = function(){
	    appService.show(url + "/Show")
               .then(function(results){
                  if(results.data !== ''){
                      $scope.resultados = results.data;//.resultado;
                      $scope.mostrar = false;
                  }
               },
               function (err){
                   console.log(err);
               });
    };
   
	$scope.delete = function () {
	    appService.delete(url + "/Delete", $scope.datos)
            .then(function (results) {
                if (results.status === 200) {

                    /*
                        *Cualquiera de los dos funciona, utilizar el que mejor les parezca 
                    */

                    /* 
                        *Itera los arreglos ya cargados en memoria y elimina el que se ha eliminado en la base de datos
                    */
                    $scope.resultados = $scope.resultados.filter(function (element) {
                        return element.Id !== $scope.datos.Id;
                    })

                    /*
                        *Llama a la funcion show despues de haber eliminado los arreglos.
                    */

                    //$scope.show();
                }
                //console.log(results);
            },
            function (err) {
                console.log(err);
            })
	}


	$scope.selectElementDelete = function (id) {

        //Abre el Dialogo
	    var dialog = document.querySelector('dialog');
	    
	    if (!dialog.showModal) {
	        dialogPolyfill.registerDialog(dialog);
	    }

	    dialog.showModal();

        //Establecemos el Id del registro a eliminar
        $scope.datos.Id = id;
    	//console.log($scope.datos.Id);
	};
    

});