import { useEffect, useState } from 'react';
import './App.css';
import 'bootstrap/dist/css/bootstrap.min.css';

function App() {
	const [operators, setOperators] = useState([]);
	const [operator, setOperator] = useState(undefined);
	const [name, setName] = useState('');
	const [code, setCode] = useState(0);
	const [error, setError] = useState();

	useEffect(() => {
		getOperators();
	}, []);

	const clearState = () => {
		setCode(0);
		setName('');
	}

	async function getOperators() {
		const fetchResponse = await fetch('operators', { method: 'GET' });

		const response = await fetchResponse.json();

		if (fetchResponse.ok) {
			setError(undefined);
			setOperators(response);
		} else {
			setError(response);
			setOperators([]);
		}

		clearState();
	}

	async function createOperator() {
		const fetchResponse = await fetch('operators', {
			body: JSON.stringify({ name: name }),
			method: 'POST',
			headers: {
				'Accept': 'application/json, text/plain',
				'Content-Type': 'application/json;charset=UTF-8'
			},
		});

		const response = await fetchResponse.json();

		if (fetchResponse.ok) {
			setError(undefined);
			getOperators();
		} else {
			setError(response);
		}

		clearState();
	}

	async function getOperator(code) {
		const fetchResponse = await fetch(`operators/${code}`, {
			method: 'GET',
			headers: {
				'Accept': 'application/json, text/plain',
				'Content-Type': 'application/json;charset=UTF-8'
			},
		});

		const response = await fetchResponse.json();

		if (fetchResponse.ok) {
			setError(undefined);
			setOperator(response);
		} else {
			setError(response);
			setOperator(undefined);
		}

		clearState();
	}

	async function updateOperator() {
		const fetchResponse = await fetch('operators', {
			body: JSON.stringify({ code: code, name: name }),
			method: 'PUT',
			headers: {
				'Accept': 'application/json, text/plain',
				'Content-Type': 'application/json;charset=UTF-8'
			},
		});

		if (fetchResponse.ok) {
			setError(undefined);
			getOperators();
		} else {
			setError(response);
		}
		
		clearState();
	}

	async function deleteOperator(code) {
		const fetchResponse = await fetch(`operators/${code}`, {
			method: 'DELETE',
			headers: {
				'Accept': 'application/json, text/plain',
				'Content-Type': 'application/json;charset=UTF-8'
			},
		});

		if (fetchResponse.ok) {
			setError(undefined);
			getOperators();
		} else {
			setError(response);
		}

		clearState();
	}

	return (
		<div className="text-start">
			<h1 id="tableLabel">Index</h1>
			<p>This component demonstrates fetching data from the server.</p>
			<div className="row">
				<div className="col-sm-6">
					<div className="input-group input-group-sm mb-3">
						<div className="input-group-prepend">
							<span className="input-group-text" id="inputGroup-sizing-sm">Name</span>
						</div>
						<input
							type="text"
							className="form-control"
							aria-label="Small"
							aria-describedby="inputGroup-sizing-sm"
							value={name}
							onChange={(e) => {
								setName(e.target.value.toString());
							}}
						/>
					</div>
				</div>
				<div className="col-sm-4">
					<button
						type="button"
						className="btn btn-link"
						data-toggle="modal"
						data-target="#exampleModal"
						disabled={!name.length}
						onClick={() => {
							code > 0 ? updateOperator() : createOperator();
						}}
					>
						{code > 0 ? 'Update' : 'Create New'}
					</button>
				</div>
			</div>
			{operator && <div className="row">
				<div className="col-sm-2">
					<strong>Details:</strong>
				</div>
				<div className="col-sm-3">
					Code: {operator.code}
				</div>
				<div className="col-sm-3">
					Name: {operator.name}
				</div>
				<div className="col-sm-3">
					<button
						className="btn btn-link"
						type="button"
						onClick={() => {
							setOperator(undefined)
						}}
					>
						Close
					</button>
				</div>
			</div>}
			{error && <div className="text-danger">
				<hr />
				<h4>Error: {error}</h4>
			</div>}
			<table className="table">
				<thead>
					<tr>
						<th scope="col">Name</th>
						<th scope="col"></th>
					</tr>
				</thead>
				<tbody>
					{operators.map((operator, index) => {
						return <tr key={index}>
							<td>{operator.name}</td>
							<td>
								<button
									className="btn btn-link"
									type="button"
									onClick={() => {
										setName(operator.name);
										setCode(operator.code);
									}}
								>
									Edit
								</button>
								&nbsp;
								|
								&nbsp;
								<button
									className="btn btn-link"
									type="button"
									onClick={() => {
										getOperator(operator.code)
									}}
								>
									Details
								</button>
								&nbsp;
								|
								&nbsp;
								<button
									className="btn btn-link"
									type="button"
									onClick={() => {
										deleteOperator(operator.code);
									}}
								>
									Delete
								</button>
							</td>
						</tr>
					})}
				</tbody>
			</table>
		</div>
	);
}

export default App;