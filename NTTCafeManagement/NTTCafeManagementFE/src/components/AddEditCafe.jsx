import React, { useEffect, useState } from 'react';
import { useNavigate, useParams } from 'react-router-dom';
import { Form, Input, Button, message, Upload } from 'antd';
import { useDispatch, useSelector } from 'react-redux';
import { fetchCafes } from '../redux/cafeSlice';
import { addCafe, updateCafe } from '../redux/cafeSlice';   

const AddEditCafe = () => {
    const [form] = Form.useForm();
    const dispatch = useDispatch();
    const navigate = useNavigate();
    const { id } = useParams();
    const cafes = useSelector((state) => state.cafes.cafes);
    const cafe = cafes.find((cafe) => cafe.id === id);

    const [image, setImage] = useState(null);

    useEffect(() => {
        if (id && !cafe) {
            dispatch(fetchCafes());
        } else if (id && cafe) {
            form.setFieldsValue(cafe);
            setImage(cafe.logo); // Assuming cafe has a logo field that holds the image URL
        }
    }, [dispatch, id, cafe, form]);

    const onFinish = (values) => {
        if (id) {
            // Update cafe
            dispatch(updateCafe({ ...values, id }));
            message.success('Cafe updated successfully');
        } else {
            // Add new cafe
            dispatch(addCafe(values));
            message.success('Cafe added successfully');
        }
        navigate('/cafes');
    };

    const handleImageUpload = (file) => {
        // Handle image upload (you could use an API or a file input for this)
        setImage(URL.createObjectURL(file));
        return false; // Prevent default upload behavior
    };

    return (
        <div style={{ padding:'2%', width: '75%' }}>
            <h1>{id ? 'Edit' : 'Add'} Cafe</h1>
            <Form
                form={form}
                onFinish={onFinish}
                initialValues={{ logo: image }}
                layout="vertical"
            >
                <Form.Item
                    label="Cafe Name"
                    name="name"
                    rules={[{ required: true, message: 'Please input the cafe name!' }]}
                >
                    <Input />
                </Form.Item>

                <Form.Item
                    label="Description"
                    name="description"
                >
                    <Input.TextArea />
                </Form.Item>

                <Form.Item
                    label="Location"
                    name="location"
                    rules={[{ required: true, message: 'Please input the location!' }]}
                >
                    <Input />
                </Form.Item>

                <Form.Item
                    label="Logo"
                    name="logo"
                >
                    <Upload
                        showUploadList={false}
                        beforeUpload={handleImageUpload}
                    >
                        <Button>Upload Logo</Button>
                    </Upload>
                    {image && <img src={image} alt="Cafe logo" style={{ width: 100, marginTop: 10 }} />}
                </Form.Item>

                <Form.Item>
                    <Button type="primary" htmlType="submit">
                        {id ? 'Update Cafe' : 'Add Cafe'}
                    </Button>
                </Form.Item>
            </Form>
        </div>
    );
};

export default AddEditCafe;
