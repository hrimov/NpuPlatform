import { Button, Card, Empty, Space, Typography } from "antd";
import { Heart, Share2 } from "lucide-react";
import type { NPUCreation } from "../types";

const { Text, Title } = Typography;

interface NPUCardProps {
  creation: NPUCreation;
  onVote: (creationId: string, score: number) => Promise<void>;
}

export function NPUCard({ creation, onVote }: NPUCardProps) {
  const handleVote = async (score: number) => {
    await onVote(creation.id, score);
  };

  console.log(creation.imageUrl);

  return (
    <Card
      cover={
        creation.imageUrl ? (
          <img
            alt={creation.title}
            src={creation.imageUrl}
            className="h-48 object-cover"
          />
        ) : (
          <div
            style={{
              height: "48rem",
              display: "flex",
              alignItems: "center",
              justifyContent: "center",
            }}
          >
            <Empty description="No Image" />
          </div>
        )
      }
      actions={[
        <Button
          key="vote"
          type="text"
          icon={<Heart size={18} />}
          onClick={() => handleVote(1)}
        >
          {creation.totalScore}
        </Button>,
        <Button key="share" type="text" icon={<Share2 size={18} />} />,
      ]}
    >
      <Title level={4}>{creation.title}</Title>
      <Space direction="vertical">
        <Text>{creation.description}</Text>
      </Space>
    </Card>
  );
}
