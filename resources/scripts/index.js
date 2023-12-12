
const url = "http://localhost:5204/api/Cars";
let myCars = [];

function handleOnLoad() {
    console.log("I made it here.");
    let html = `
        <div class="container-fluid py-5">
            <h1 class="subheader display-5 fw-bold" style="text-align:center;">Julio Jones Kia</h1>
            <p class="text1 col-md-auto" style="text-align: center">"Track your car inventory today with a click of a button"</p>
            <div class="jumbo-img" style="text-align: center;">
                <img src="./images/KiaMoters.png" class="imgMAin" alt="kia-moters-600x300" width="500" height="250">
            </div>
            <br>
            <div id="tableBody"></div>

            <button onclick="displayAddForm()" class="btn btn-primary">Add Car</button>
            
            <div id="addForm" style="display: none;">
                <form onsubmit="handleCarAdd(); return false;">
                    <label for="make">Make:</label><br>
                    <input type="text" id="make" name="make"><br>
                    <label for="model">Model:</label><br>
                    <input type="text" id="model" name="model"><br>
                    <label for="mileage">Mileage:</label><br>
                    <input type="number" id="mileage" name="mileage" step="0.01" min="0"><br><br>        
                    <button type="submit" class="btn btn-primary">Submit</button>
                </form>
            </div>
        </div>`;

    document.getElementById('app').innerHTML = html;
    populateTable();
}

function displayAddForm() {
    document.getElementById('addForm').style.display = 'block';
}

async function populateTable() {
    try {
        const cars = await getAllCars();
        console.log(cars);
        cars.sort((a, b) => new Date(a.date) - new Date(b.date));
        let html = `
            <table class="table table-striped">
                <tr>
                    <th>Make</th>
                    <th>Model</th>
                    <th>Mileage</th>
                    <th>Date In Inventory</th>
                    <th>Hold</th>
                    <th>Delete</th>
                </tr>`;

        cars.forEach(function (car) {
            if (car.mileage === undefined) {
                car.mileage = 0;
            }
            if (!car.deleted) {
                html += `
                    <tr>
                        <td>${car.make}</td>
                        <td>${car.model}</td>
                        <td>${car.mileage}</td>
                        <td>${car.date}</td>
                        <td><input type="checkbox" ${car.hold ? 'checked' : ''} onchange="handleHoldConfirmation(this, '${car.carID}')"></td>
                        <td><button class="btn btn-danger" onclick="handleCarDelete('${car.carID}')">Delete</button></td>
                    </tr>`;
            }
        });

        html += `</table>`;
        document.getElementById('tableBody').innerHTML = html;
    } catch (error) {
        console.error('Error fetching cars:', error);
    }
}

function handleHoldConfirmation(checkbox, carID) {
    const confirmation = confirm("Are you sure you want to hold this car?");
    if (confirmation) {
        handleCarHold(carID);
    } else {
        checkbox.checked = !checkbox.checked;
    }
}

async function handleCarHold(carID) {
    try {
        const response = await fetch(url + "/" + carID);
        const car = await response.json();
        car.hold = true;
        await saveCar(car);
        populateTable();
    } catch (error) {
        console.error('Error holding car:', error);
    }
}

async function handleCarAdd() {
    const tempDate = new Date();
    let tempString = ""
    tempString = tempDate.getFullYear() + "-" + tempDate.getMonth() + "-" + tempDate.getDate() + " " + tempDate.getHours() + ":" + tempDate.getMinutes() + ":" + tempDate.getSeconds()
    let mileage = parseFloat(document.getElementById('mileage').value); // Parse the distance as a float
    mileage = Math.round(mileage * 100) / 100; // Round to two decimal places
    let car = {
        Make: document.getElementById('make').value,
        Model: document.getElementById('model').value,
        Mileage: mileage,
        Date: tempString,
        Hold: false,
        Deleted: false,
    };
    console.log("What car am I adding?", car);
    myCars.push(car);
    await saveCar(car);
    document.getElementById('make').value = '';
    document.getElementById('model').value = '';
    document.getElementById('mileage').value = '';
    populateTable();
}

async function saveCar(car) {
    console.log("what car am I saving", car);
    if (car.carID) {
        const updateUrl = `${url}/${car.carID}`;
        await fetch(updateUrl, {
            method: "PUT", 
            body: JSON.stringify(car),
            headers: {
                "Content-type": "application/json; charset=UTF-8"
            }
        });
    } else {
        await fetch(url, {
            method: "POST",
            body: JSON.stringify(car),
            headers: {
                "Content-type": "application/json; charset=UTF-8"
            }
        });
    }
}

async function handleCarDelete(carID) {
    carID--
    try {
        const myCars = await getAllCars();
        myCars.forEach(car => {
            if(car.carID == carID)
            {
                car.deleted = true;
            }
        });
        console.log(myCars[carID])
        deleteCar(myCars[carID]);
    } catch (error) {
        console.error('Error removing from product:', error);
    }
}

async function deleteCar(car)
{
    const putUrl = url +"/"+ car.carID;
    console.log(car.carID)
    car.deleted = true;
    console.log(car)
    await fetch(putUrl, {
        method: "PUT",
        headers: {
            'Accept': 'application/json',
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(car)
    })
    .then((response)=>{
        console.log(response)
    })
    populateTable();
}

async function getAllCars() {
    try {
        const response = await fetch(url);
        const Cars = await response.json();
        return Cars;
    } catch (error) {
        console.error('Error fetching Cars:', error);
    }
}

