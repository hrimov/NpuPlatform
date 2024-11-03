import { Form, Input, Button, Card, Typography, message } from "antd";
import { useNavigate, Link, useLocation } from "react-router-dom";
import { login } from "../services/auth";
import { useAuth } from "../contexts/AuthContext";
import type { LoginCredentials } from "../types/auth";

const { Title } = Typography;

export default function Login() {
  const navigate = useNavigate();
  const location = useLocation();
  const { setUser } = useAuth();
  const from = location.state?.from?.pathname || "/";

  const onFinish = async (values: LoginCredentials) => {
    try {
      const response = await login(values);
      setUser(response.user);
      message.success("Login successful!");
      navigate(from, { replace: true });
    } catch (error) {
      console.error(error);
    }
  };

  return (
    <div className="min-h-screen flex items-center justify-center bg-gray-50 py-12 px-4 sm:px-6 lg:px-8">
      <Card className="w-full max-w-md">
        <div className="text-center mb-8">
          <Title level={2}>Welcome Back</Title>
          <Typography.Text type="secondary">
            Please sign in to your account
          </Typography.Text>
        </div>

        <Form
          name="login"
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
            label="Password"
            name="password"
            rules={[{ required: true, message: "Please input your password!" }]}
          >
            <Input.Password size="large" />
          </Form.Item>

          <Form.Item>
            <Button type="primary" htmlType="submit" size="large" block>
              Sign in
            </Button>
          </Form.Item>

          <div className="text-center">
            <Typography.Text>
              Don't have an account?{" "}
              <Link
                to="/register"
                className="text-blue-600 hover:text-blue-500"
              >
                Register now
              </Link>
            </Typography.Text>
          </div>
        </Form>
      </Card>
    </div>
  );
}
