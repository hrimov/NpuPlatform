import { Form, Input, Button, Card, Typography, message } from "antd";
import { useNavigate, Link } from "react-router-dom";
import { register } from "../services/auth";
import type { RegisterCredentials } from "../types/auth";

const { Title } = Typography;

export default function Register() {
  const navigate = useNavigate();

  const onFinish = async (values: RegisterCredentials) => {
    try {
      const response = await register(values);
      if (response.username) {
        message.success("Registration successful!");
      } else {
        message.error("Registration failed. Please try again.");
      }
      navigate("/login", { replace: true });
    } catch (error) {
      console.error(error);
    }
  };

  return (
    <div className="min-h-screen flex items-center justify-center bg-gray-50 py-12 px-4 sm:px-6 lg:px-8">
      <Card className="w-full max-w-md">
        <div className="text-center mb-8">
          <Title level={2}>Create Account</Title>
          <Typography.Text type="secondary">
            Join our NPU platform
          </Typography.Text>
        </div>

        <Form
          name="register"
          layout="vertical"
          onFinish={onFinish}
          autoComplete="off"
        >
          <Form.Item
            label="Username"
            name="username"
            rules={[{ required: true, message: "Please input your username!" }]}
          >
            <Input size="large" />
          </Form.Item>

          <Form.Item
            label="Email"
            name="email"
            rules={[
              { required: true, message: "Please input your email!" },
              { type: "email", message: "Please enter a valid email!" },
            ]}
          >
            <Input size="large" />
          </Form.Item>

          <Form.Item
            label="Password"
            name="password"
            rules={[{ required: true, message: "Please input your password!" }]}
          >
            <Input.Password size="large" />
          </Form.Item>

          <Form.Item>
            <Button type="primary" htmlType="submit" size="large" block>
              Register
            </Button>
          </Form.Item>

          <div className="text-center">
            <Typography.Text>
              Already have an account?{" "}
              <Link to="/login" className="text-blue-600 hover:text-blue-500">
                Sign in
              </Link>
            </Typography.Text>
          </div>
        </Form>
      </Card>
    </div>
  );
}
