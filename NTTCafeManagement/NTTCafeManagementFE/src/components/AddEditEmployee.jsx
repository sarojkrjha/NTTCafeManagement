import React, { useEffect, useState } from 'react';
import { useNavigate, useParams } from 'react-router-dom';
import { Form, Input, Button, message, Select } from 'antd';
import { useDispatch, useSelector } from 'react-redux';
import { fetchEmployees } from '../redux/employeeSlice';
import { addEmployee, updateEmployee } from '../redux/employeeSlice';  // You will need to create updateEmployee action.

const AddEditEmployee = () => {
    const [form] = Form.useForm();
    const dispatch = useDispatch();
    const navigate = useNavigate();
    const { id } = useParams();
    const employees = useSelector((state) => state.employees.employees);
    const cafes = useSelector((state) => state.cafes.cafes);
    const employee = employees.find((emp) => emp.id === id);

    useEffect(() => {
        if (id && !employee) {
            dispatch(fetchEmployees());
        } else if (id && employee) {
            form.setFieldsValue(employee);
        }
    }, [dispatch, id, employee, form]);

    const onFinish = (values) => {
        if (id) {
            // Update employee
            dispatch(updateEmployee({ ...values, id }));
            message.success('Employee updated successfully');
        } else {
            // Add new employee
            dispatch(addEmployee(values));
            message.success('Employee added successfully');
        }
        navigate('/employees');
    };

    return (
        <div style={{ padding: '2%', width: '75%' }}>
            <h1>{id ? 'Edit' : 'Add'} Employee</h1>
            <Form
                form={form}
                onFinish={onFinish}
                layout="vertical"
            >
                <Form.Item
                    label="Employee Name"
                    name="name"
                    rules={[{ required: true, message: 'Please input the employee name!' }]}
                >
                    <Input />
                </Form.Item>

                <Form.Item
                    label="Age"
                    name="age"
                    rules={[{ required: true, message: 'Please input the employee age!' }]}
                >
                    <Input />
                </Form.Item>

                <Form.Item
                    label="Position"
                    name="position"
                    rules={[{ required: true, message: 'Please input the employee position!' }]}
                >
                    <Input />
                </Form.Item>

                <Form.Item
                    label="Café"
                    name="cafeId"
                    rules={[{ required: true, message: 'Please select a café!' }]}
                >
                    <Select>
                        {cafes.map((cafe) => (
                            <Select.Option key={cafe.id} value={cafe.id}>
                                {cafe.name}
                            </Select.Option>
                        ))}
                    </Select>
                </Form.Item>

                <Form.Item>
                    <Button type="primary" htmlType="submit">
                        {id ? 'Update Employee' : 'Add Employee'}
                    </Button>
                </Form.Item>
            </Form>
        </div>
    );
};

export default AddEditEmployee;
