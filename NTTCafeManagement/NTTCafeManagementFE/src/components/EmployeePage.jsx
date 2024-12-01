import React, { useEffect } from 'react';
import { useSelector, useDispatch } from 'react-redux';
import { fetchEmployees } from '../redux/employeeSlice';
import { Button, Table } from 'antd';
import { useNavigate } from 'react-router-dom';

const EmployeePage = () => {
    const { employees, loading } = useSelector((state) => state.employees);
    const dispatch = useDispatch();
    const navigate = useNavigate();

    useEffect(() => {
        dispatch(fetchEmployees());
    }, [dispatch]);

    const handleDelete = (id) => {
        // Call API to delete the employee
        console.log(`Delete employee with ID: ${id}`);
    };
   
    const columns = [
        { title: 'Employee ID', dataIndex: 'id', key: 'id' },
        { title: 'Name', dataIndex: 'name', key: 'name' },
        { title: 'Email Address', dataIndex: 'email_Address', key: 'email_Address' },
        { title: 'Phone Number', dataIndex: 'phone_Number', key: 'phone_Number' },
        { title: 'Days Worked', dataIndex: 'daysWorked', key: 'daysWorked' },
        { title: 'Cafe Name', dataIndex: 'cafe', key: 'cafe' },
        {
            title: 'Actions',
            key: 'actions',
            render: (_, record) => (
                <>
                    <Button
                        onClick={() => navigate(`/employees/edit/${record.id}`)}
                        style={{ marginRight: 8 }}
                    >
                        Edit
                    </Button>
                    <Button type="danger" onClick={() => handleDelete(record.id)}>
                        Delete
                    </Button>
                </>
            ),
        },
    ];

    return (
        <div style={{ padding: '2%', width: '75%' }}>
            <div style={{ marginBottom: 16, display: 'flex', justifyContent: 'space-between' }}>
                <h2>Employees</h2>
                <Button
                    type="primary"
                    onClick={() => navigate('/employees/add')}
                >
                    Add New Employee
                </Button>
            </div>
            <Table
                dataSource={employees}
                columns={columns}
                rowKey="id"
                loading={loading}
            />
        </div>
    );
};

export default EmployeePage;
